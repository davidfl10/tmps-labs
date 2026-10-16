using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.App.Services.Pricing;

public class StudentCardDiscount : IDiscountPolicy
{
    private const decimal Rate = 0.15m;

    public string Description => "15% off for holders of a student card";

    public decimal Apply(decimal currentPrice, Student student, ISubscription subscription) =>
        student.HasStudentCard ? currentPrice * (1 - Rate) : currentPrice;
}
