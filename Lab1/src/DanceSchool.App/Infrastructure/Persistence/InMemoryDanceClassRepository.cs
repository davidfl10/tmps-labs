using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Infrastructure.Persistence;

public class InMemoryDanceClassRepository : IDanceClassRepository
{
    private readonly Dictionary<int, DanceClass> _classes = [];

    public DanceClass? GetById(int id) => _classes.GetValueOrDefault(id);

    public IReadOnlyList<DanceClass> GetAll() => _classes.Values.OrderBy(c => c.StartsAt).ToList();

    public void Add(DanceClass danceClass)
    {
        if (!_classes.TryAdd(danceClass.Id, danceClass))
            throw new InvalidOperationException($"A class with id {danceClass.Id} already exists.");
    }
}
