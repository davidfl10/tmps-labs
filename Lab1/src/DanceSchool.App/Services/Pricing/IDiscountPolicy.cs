using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Services.Pricing;

/// <summary>
/// OCP extension point: a new promotion is a new policy registered in the composition root,
/// <see cref="PriceCalculator"/> stays untouched.
/// </summary>
public interface IDiscountPolicy
{
    string Description { get; }
    decimal Apply(decimal currentPrice, Student student, ISubscription subscription);
}
