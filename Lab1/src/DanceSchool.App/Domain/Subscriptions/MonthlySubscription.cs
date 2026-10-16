namespace DanceSchool.App.Domain.Subscriptions;

public class MonthlySubscription : ISubscription
{
    public const int ClassesIncluded = 8;

    public string Name => $"monthly subscription ({ClassesIncluded} classes)";
    public decimal BasePrice => 600m;
    public bool CanAttend(int classesAlreadyAttended) => classesAlreadyAttended < ClassesIncluded;
}
