using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;
using DanceSchool.App.Services.Pricing;

namespace DanceSchool.Tests;

public class PriceCalculatorTests
{
    private static readonly Student Regular = new(1, "Ion", "Rusu", "ion@mail.md", "+373 1");
    private static readonly Student WithCard = new(2, "Ana", "Popescu", "ana@mail.md", "+373 2", HasStudentCard: true);

    [Fact]
    public void Without_policies_returns_base_price()
    {
        var calculator = new PriceCalculator([]);

        Assert.Equal(600m, calculator.Calculate(WithCard, new MonthlySubscription()));
    }

    [Fact]
    public void Student_card_gives_15_percent_off()
    {
        var calculator = new PriceCalculator([new StudentCardDiscount()]);

        Assert.Equal(850m, calculator.Calculate(WithCard, new UnlimitedSubscription()));
        Assert.Equal(1000m, calculator.Calculate(Regular, new UnlimitedSubscription()));
    }

    [Fact]
    public void Discounts_are_stacked_in_order()
    {
        var calculator = new PriceCalculator([new StudentCardDiscount(), new LargePassDiscount()]);

        // 10 x 90 = 900 -> -15% = 765 -> -10% = 688.50
        Assert.Equal(688.50m, calculator.Calculate(WithCard, new PerClassPass(10)));
    }

    [Fact]
    public void Large_pass_discount_ignores_small_passes()
    {
        var calculator = new PriceCalculator([new LargePassDiscount()]);

        Assert.Equal(360m, calculator.Calculate(Regular, new PerClassPass(4)));
    }
}
