using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Services.Pricing;

public class LargePassDiscount : IDiscountPolicy
{
    private const int MinClasses = 10;
    private const decimal Rate = 0.10m;

    public string Description => $"10% off passes of {MinClasses}+ classes";

    public decimal Apply(decimal currentPrice, Student student, ISubscription subscription) =>
        subscription is PerClassPass { Classes: >= MinClasses } ? currentPrice * (1 - Rate) : currentPrice;
}
