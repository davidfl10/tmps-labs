using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Infrastructure.Persistence;

public static class SeedData
{
    public static void Populate(IStudentRepository students, IDanceClassRepository classes)
    {
        students.Add(new Student(1, "Ana", "Popescu", "ana.popescu@mail.md", "+373 69 111 111", HasStudentCard: true));
        students.Add(new Student(2, "Ion", "Rusu", "ion.rusu@mail.md", "+373 69 222 222"));
        students.Add(new Student(3, "Maria", "Ceban", "maria.ceban@mail.md", "+373 69 333 333", HasStudentCard: true));
        students.Add(new Student(4, "Victor", "Lungu", "victor.lungu@mail.md", "+373 69 444 444"));

        var elena = new Instructor(1, "Elena", "Moraru", DanceStyle.Salsa);
        var andrei = new Instructor(2, "Andrei", "Munteanu", DanceStyle.HipHop);

        classes.Add(new DanceClass(1, "Salsa Beginners", DanceStyle.Salsa, elena, new DateTime(2026, 10, 19, 18, 0, 0), capacity: 10));
        classes.Add(new DanceClass(2, "Bachata Sensual", DanceStyle.Bachata, elena, new DateTime(2026, 10, 20, 19, 30, 0), capacity: 8));
        classes.Add(new DanceClass(3, "Hip-Hop Choreo", DanceStyle.HipHop, andrei, new DateTime(2026, 10, 21, 17, 0, 0), capacity: 2));
    }
}
