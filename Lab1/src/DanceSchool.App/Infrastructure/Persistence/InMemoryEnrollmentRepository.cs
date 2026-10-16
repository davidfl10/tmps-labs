using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Infrastructure.Persistence;

public class InMemoryEnrollmentRepository : IEnrollmentRepository
{
    private readonly List<Enrollment> _enrollments = [];

    public void Add(Enrollment enrollment) => _enrollments.Add(enrollment);

    public Enrollment? Find(int studentId, int classId) =>
        _enrollments.FirstOrDefault(e => e.Student.Id == studentId && e.DanceClass.Id == classId);

    public IReadOnlyList<Enrollment> GetByClass(int classId) =>
        _enrollments.Where(e => e.DanceClass.Id == classId).ToList();
}
