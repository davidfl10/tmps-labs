using DanceSchool.App.Domain;

namespace DanceSchool.App.Abstractions;

public interface IEnrollmentRepository
{
    void Add(Enrollment enrollment);
    Enrollment? Find(int studentId, int classId);
    IReadOnlyList<Enrollment> GetByClass(int classId);
}
