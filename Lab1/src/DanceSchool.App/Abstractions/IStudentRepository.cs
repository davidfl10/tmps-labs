using DanceSchool.App.Domain;

namespace DanceSchool.App.Abstractions;

public interface IStudentRepository
{
    Student? GetById(int id);
    IReadOnlyList<Student> GetAll();
    void Add(Student student);
}
