using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.App.Infrastructure.Notifications;

/// <summary>Simulated e-mail channel: writes the message instead of calling an SMTP server.</summary>
public class EmailNotificationSender : INotificationSender
{
    private readonly TextWriter _output;

    public EmailNotificationSender(TextWriter output)
    {
        _output = output;
    }

    public void Send(Student recipient, string message) =>
        _output.WriteLine($"    [EMAIL -> {recipient.Email}] {message}");
}
