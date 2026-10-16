namespace DanceSchool.App.Domain;

/// <summary>
/// A scheduled group class. It only guards its own invariants (capacity, no duplicates) - SRP.
/// </summary>
public class DanceClass
{
    private readonly HashSet<int> _enrolledStudentIds = [];

    public DanceClass(int id, string name, DanceStyle style, Instructor instructor, DateTime startsAt, int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");

        Id = id;
        Name = name;
        Style = style;
        Instructor = instructor;
        StartsAt = startsAt;
        Capacity = capacity;
    }

    public int Id { get; }
    public string Name { get; }
    public DanceStyle Style { get; }
    public Instructor Instructor { get; }
    public DateTime StartsAt { get; }
    public int Capacity { get; }

    public IReadOnlyCollection<int> EnrolledStudentIds => _enrolledStudentIds;
    public int FreeSpots => Capacity - _enrolledStudentIds.Count;
    public bool IsFull => FreeSpots == 0;

    public void AddStudent(int studentId)
    {
        if (IsFull)
            throw new EnrollmentException($"Class '{Name}' is full ({Capacity}/{Capacity}).");
        if (!_enrolledStudentIds.Add(studentId))
            throw new EnrollmentException($"Student #{studentId} is already enrolled in '{Name}'.");
    }
}
