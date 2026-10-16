using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Services;

/// <summary>SRP: handles check-ins only.</summary>
public class AttendanceService
{
    private readonly IEnrollmentRepository _enrollments;
    private readonly INotificationSender _notifier;

    public AttendanceService(IEnrollmentRepository enrollments, INotificationSender notifier)
    {
        _enrollments = enrollments;
        _notifier = notifier;
    }

    /// <returns><c>true</c> if the student was let in, <c>false</c> if the subscription is used up.</returns>
    public bool CheckIn(int studentId, int classId)
    {
        var enrollment = _enrollments.Find(studentId, classId)
            ?? throw new EnrollmentException($"Student #{studentId} is not enrolled in class #{classId}.");

        if (!enrollment.CanAttendNextClass)
        {
            _notifier.Send(enrollment.Student,
                $"Your {enrollment.Subscription.Name} for '{enrollment.DanceClass.Name}' is used up. Renew it to keep dancing!");
            return false;
        }

        enrollment.RegisterAttendance();
        return true;
    }
}
