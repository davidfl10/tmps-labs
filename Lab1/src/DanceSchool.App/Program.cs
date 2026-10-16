using System.Globalization;
using DanceSchool.App.Domain;
using DanceSchool.App.Domain.Subscriptions;
using DanceSchool.App.Infrastructure.Notifications;
using DanceSchool.App.Infrastructure.Persistence;
using DanceSchool.App.Presentation;
using DanceSchool.App.Services;
using DanceSchool.App.Services.Pricing;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
var output = Console.Out;

// Composition root: the only place that knows the concrete classes (DIP).
var students = new InMemoryStudentRepository();
var classes = new InMemoryDanceClassRepository();
var enrollments = new InMemoryEnrollmentRepository();
SeedData.Populate(students, classes);

var priceCalculator = new PriceCalculator([new StudentCardDiscount(), new LargePassDiscount()]);
var enrollmentService = new EnrollmentService(
    students, classes, enrollments, priceCalculator, new EmailNotificationSender(output), TimeProvider.System);
var attendanceService = new AttendanceService(enrollments, new SmsNotificationSender(output));
var report = new ScheduleReportPrinter(classes, enrollments, output);

Section("1. Enrollments (price = subscription + discount policies)");
TryEnroll(1, 1, new PerClassPass(10));        // student card + large pass discounts
TryEnroll(2, 1, new MonthlySubscription());   // no discount
TryEnroll(3, 2, new UnlimitedSubscription()); // student card discount
TryEnroll(4, 2, new PerClassPass(2));
TryEnroll(1, 3, new PerClassPass(4));
TryEnroll(2, 3, new MonthlySubscription());

Section("2. Business rules");
TryEnroll(3, 3, new PerClassPass(4)); // class is full
TryEnroll(2, 1, new PerClassPass(4)); // already enrolled
TryEnroll(9, 1, new PerClassPass(4)); // unknown student

Section("3. Check-ins (Victor has a 2-class pass)");
for (var i = 1; i <= 3; i++)
{
    var allowed = attendanceService.CheckIn(4, 2);
    output.WriteLine($"  Check-in #{i} for Victor Lungu: {(allowed ? "welcome!" : "denied")}");
}
attendanceService.CheckIn(1, 1);
attendanceService.CheckIn(3, 2);

Section("4. Weekly schedule");
report.Print();

void TryEnroll(int studentId, int classId, ISubscription subscription)
{
    try
    {
        var enrollment = enrollmentService.Enroll(studentId, classId, subscription);
        output.WriteLine($"  OK    {enrollment.Student.FullName} -> {enrollment.DanceClass.Name}");
    }
    catch (EnrollmentException ex)
    {
        output.WriteLine($"  ERROR {ex.Message}");
    }
}

void Section(string title)
{
    output.WriteLine();
    output.WriteLine($"=== {title} ===");
}
