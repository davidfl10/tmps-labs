using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Infrastructure.Persistence;

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly Dictionary<int, Student> _students = [];

    public Student? GetById(int id) => _students.GetValueOrDefault(id);

    public IReadOnlyList<Student> GetAll() => _students.Values.ToList();

    public void Add(Student student)
    {
        if (!_students.TryAdd(student.Id, student))
            throw new InvalidOperationException($"A student with id {student.Id} already exists.");
    }
}
