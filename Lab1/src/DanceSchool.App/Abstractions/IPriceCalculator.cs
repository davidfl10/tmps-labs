using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Abstractions;

public interface IPriceCalculator
{
    decimal Calculate(Student student, ISubscription subscription);
}
