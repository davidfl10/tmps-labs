using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Services.Pricing;

/// <summary>SRP: only computes the final price. Closed for modification, open through <see cref="IDiscountPolicy"/>.</summary>
public class PriceCalculator : IPriceCalculator
{
    private readonly IReadOnlyList<IDiscountPolicy> _discountPolicies;

    public PriceCalculator(IEnumerable<IDiscountPolicy> discountPolicies)
    {
        _discountPolicies = discountPolicies.ToList();
    }

    public decimal Calculate(Student student, ISubscription subscription)
    {
        var price = subscription.BasePrice;
        foreach (var policy in _discountPolicies)
            price = policy.Apply(price, student, subscription);

        return Math.Round(price, 2);
    }
}
