namespace DanceSchool.App.Domain.Subscriptions;

/// <summary>
/// OCP extension point: a new kind of subscription is a new class implementing this interface,
/// no existing class has to be modified.
/// </summary>
public interface ISubscription
{
    string Name { get; }
    decimal BasePrice { get; }
    bool CanAttend(int classesAlreadyAttended);
}
