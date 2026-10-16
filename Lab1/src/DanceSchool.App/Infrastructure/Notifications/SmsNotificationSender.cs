using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Infrastructure.Notifications;

/// <summary>Simulated SMS channel: writes the message instead of calling an SMS gateway.</summary>
public class SmsNotificationSender : INotificationSender
{
    private readonly TextWriter _output;

    public SmsNotificationSender(TextWriter output)
    {
        _output = output;
    }

    public void Send(Student recipient, string message) =>
        _output.WriteLine($"    [SMS -> {recipient.Phone}] {message}");
}
