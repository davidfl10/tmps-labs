using DanceSchool.App.Abstractions;

namespace DanceSchool.App.Presentation;

/// <summary>SRP: only formats the weekly schedule; it does not change any data.</summary>
public class ScheduleReportPrinter
{
    private readonly IDanceClassRepository _classes;
    private readonly IEnrollmentRepository _enrollments;
    private readonly TextWriter _output;

    public ScheduleReportPrinter(IDanceClassRepository classes, IEnrollmentRepository enrollments, TextWriter output)
    {
        _classes = classes;
        _enrollments = enrollments;
        _output = output;
    }

    public void Print()
    {
        foreach (var danceClass in _classes.GetAll())
        {
            var taken = danceClass.Capacity - danceClass.FreeSpots;
            _output.WriteLine(
                $"  {danceClass.StartsAt:ddd HH:mm}  {danceClass.Name,-16} {danceClass.Style,-8} " +
                $"with {danceClass.Instructor.FullName,-16} {taken}/{danceClass.Capacity} spots");

            foreach (var enrollment in _enrollments.GetByClass(danceClass.Id))
            {
                _output.WriteLine(
                    $"      - {enrollment.Student.FullName,-13} {enrollment.Subscription.Name,-36} " +
                    $"attended {enrollment.ClassesAttended,2}   paid {enrollment.FinalPrice,8:0.00} MDL");
            }
        }
    }
}
