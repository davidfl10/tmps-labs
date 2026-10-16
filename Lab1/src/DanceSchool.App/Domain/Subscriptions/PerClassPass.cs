namespace DanceSchool.App.Domain.Subscriptions;

public class PerClassPass : ISubscription
{
    public PerClassPass(int classes, decimal pricePerClass = 90m)
    {
        if (classes <= 0)
            throw new ArgumentOutOfRangeException(nameof(classes), "A pass must include at least one class.");

        Classes = classes;
        PricePerClass = pricePerClass;
    }

    public int Classes { get; }
    public decimal PricePerClass { get; }

    public string Name => $"{Classes}-class pass";
    public decimal BasePrice => Classes * PricePerClass;
    public bool CanAttend(int classesAlreadyAttended) => classesAlreadyAttended < Classes;
}
