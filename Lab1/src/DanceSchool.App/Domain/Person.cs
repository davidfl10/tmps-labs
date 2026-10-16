namespace DanceSchool.App.Domain;

public abstract record Person(int Id, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}
