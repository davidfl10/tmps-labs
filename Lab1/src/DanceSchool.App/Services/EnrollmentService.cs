using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Services;

/// <summary>
/// SRP: coordinates the enrollment use case only.
/// DIP: depends on abstractions; the concrete storage and notification channel are injected.
/// </summary>
public class EnrollmentService
{
    private readonly IStudentRepository _students;
    private readonly IDanceClassRepository _classes;
    private readonly IEnrollmentRepository _enrollments;
    private readonly IPriceCalculator _priceCalculator;
    private readonly INotificationSender _notifier;
    private readonly TimeProvider _timeProvider;

    public EnrollmentService(
        IStudentRepository students,
        IDanceClassRepository classes,
        IEnrollmentRepository enrollments,
        IPriceCalculator priceCalculator,
        INotificationSender notifier,
        TimeProvider timeProvider)
    {
        _students = students;
        _classes = classes;
        _enrollments = enrollments;
        _priceCalculator = priceCalculator;
        _notifier = notifier;
        _timeProvider = timeProvider;
    }

    public Enrollment Enroll(int studentId, int classId, ISubscription subscription)
    {
        var student = _students.GetById(studentId)
            ?? throw new EnrollmentException($"Student #{studentId} does not exist.");
        var danceClass = _classes.GetById(classId)
            ?? throw new EnrollmentException($"Class #{classId} does not exist.");

        danceClass.AddStudent(student.Id);

        var price = _priceCalculator.Calculate(student, subscription);
        var enrollment = new Enrollment(student, danceClass, subscription, price, _timeProvider.GetLocalNow().DateTime);
        _enrollments.Add(enrollment);

        _notifier.Send(student,
            $"You are enrolled in '{danceClass.Name}' ({danceClass.StartsAt:ddd dd MMM, HH:mm}) " +
            $"Plan: {subscription.Name}. Price: {price:0.00} MDL.");

        return enrollment;
    }
}
