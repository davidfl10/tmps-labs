namespace DanceSchool.App.Domain;

public record Student(int Id, string FirstName, string LastName, string Email, string Phone, bool HasStudentCard = false)
    : Person(Id, FirstName, LastName);
