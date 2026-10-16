using DanceSchool.App.Domain;

namespace DanceSchool.App.Abstractions;

public interface IDanceClassRepository
{
    DanceClass? GetById(int id);
    IReadOnlyList<DanceClass> GetAll();
    void Add(DanceClass danceClass);
}
