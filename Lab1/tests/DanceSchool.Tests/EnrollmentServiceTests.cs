using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;
using DanceSchool.App.Infrastructure.Persistence;
using DanceSchool.App.Services;
using DanceSchool.App.Services.Pricing;

namespace DanceSchool.Tests;

public class EnrollmentServiceTests
{
    private readonly InMemoryStudentRepository _students = new();
    private readonly InMemoryDanceClassRepository _classes = new();
    private readonly InMemoryEnrollmentRepository _enrollments = new();
    private readonly FakeNotificationSender _notifier = new();
    private readonly EnrollmentService _service;

    public EnrollmentServiceTests()
    {
        var instructor = new Instructor(1, "Elena", "Moraru", DanceStyle.Salsa);
        _students.Add(new Student(1, "Ana", "Popescu", "ana@mail.md", "+373 1", HasStudentCard: true));
        _students.Add(new Student(2, "Ion", "Rusu", "ion@mail.md", "+373 2"));
        _classes.Add(new DanceClass(1, "Salsa", DanceStyle.Salsa, instructor, new DateTime(2026, 10, 19, 18, 0, 0), capacity: 1));

        _service = new EnrollmentService(
            _students, _classes, _enrollments,
            new PriceCalculator([new StudentCardDiscount()]),
            _notifier, TimeProvider.System);
    }

    [Fact]
    public void Enroll_saves_enrollment_with_discounted_price_and_notifies_student()
    {
        var enrollment = _service.Enroll(1, 1, new MonthlySubscription());

        Assert.Equal(510m, enrollment.FinalPrice);
        Assert.Same(enrollment, _enrollments.Find(1, 1));
        var (recipient, message) = Assert.Single(_notifier.Sent);
        Assert.Equal(1, recipient.Id);
        Assert.Contains("510.00 MDL", message);
    }

    [Fact]
    public void Enroll_fails_when_class_is_full()
    {
        _service.Enroll(1, 1, new MonthlySubscription());

        var ex = Assert.Throws<EnrollmentException>(() => _service.Enroll(2, 1, new MonthlySubscription()));
        Assert.Contains("full", ex.Message);
        Assert.Single(_notifier.Sent);
    }

    [Fact]
    public void Enroll_fails_for_unknown_student()
    {
        Assert.Throws<EnrollmentException>(() => _service.Enroll(99, 1, new MonthlySubscription()));
        Assert.Empty(_notifier.Sent);
    }

    [Fact]
    public void Check_in_is_denied_once_pass_is_used_up()
    {
        _service.Enroll(2, 1, new PerClassPass(1));
        var attendance = new AttendanceService(_enrollments, _notifier);

        Assert.True(attendance.CheckIn(2, 1));
        Assert.False(attendance.CheckIn(2, 1));
        Assert.Contains("used up", _notifier.Sent[^1].Message);
    }
}
