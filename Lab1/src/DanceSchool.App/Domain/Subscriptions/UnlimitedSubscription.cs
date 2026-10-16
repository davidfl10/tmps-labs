namespace DanceSchool.App.Domain.Subscriptions;

public class UnlimitedSubscription : ISubscription
{
    public string Name => "unlimited monthly subscription";
    public decimal BasePrice => 1000m;
    public bool CanAttend(int classesAlreadyAttended) => true;
}
