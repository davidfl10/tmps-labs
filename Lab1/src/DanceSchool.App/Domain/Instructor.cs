namespace DanceSchool.App.Domain;

public record Instructor(int Id, string FirstName, string LastName, DanceStyle Specialty)
    : Person(Id, FirstName, LastName);
