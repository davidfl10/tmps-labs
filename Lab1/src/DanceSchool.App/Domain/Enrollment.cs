using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Domain;

public class Enrollment
{
    public Enrollment(Student student, DanceClass danceClass, ISubscription subscription, decimal finalPrice, DateTime enrolledAt)
    {
        Student = student;
        DanceClass = danceClass;
        Subscription = subscription;
        FinalPrice = finalPrice;
        EnrolledAt = enrolledAt;
    }

    public Student Student { get; }
    public DanceClass DanceClass { get; }
    public ISubscription Subscription { get; }
    public decimal FinalPrice { get; }
    public DateTime EnrolledAt { get; }
    public int ClassesAttended { get; private set; }

    public bool CanAttendNextClass => Subscription.CanAttend(ClassesAttended);

    public void RegisterAttendance()
    {
        if (!CanAttendNextClass)
            throw new EnrollmentException($"{Subscription.Name} of {Student.FullName} is used up.");

        ClassesAttended++;
    }
}
