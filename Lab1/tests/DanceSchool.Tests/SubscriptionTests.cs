using DanceSchool.App.Domain.Subscriptions;

namespace DanceSchool.Tests;

public class SubscriptionTests
{
    [Fact]
    public void Per_class_pass_allows_exactly_its_number_of_classes()
    {
        var pass = new PerClassPass(2);

        Assert.True(pass.CanAttend(0));
        Assert.True(pass.CanAttend(1));
        Assert.False(pass.CanAttend(2));
    }

    [Fact]
    public void Monthly_subscription_allows_eight_classes()
    {
        var monthly = new MonthlySubscription();

        Assert.True(monthly.CanAttend(7));
        Assert.False(monthly.CanAttend(8));
    }

    [Fact]
    public void Unlimited_subscription_never_runs_out()
    {
        Assert.True(new UnlimitedSubscription().CanAttend(1_000));
    }

    [Fact]
    public void Per_class_pass_rejects_zero_classes()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new PerClassPass(0));
    }
}
