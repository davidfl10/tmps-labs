using DanceSchool.App.Abstractions;
using DanceSchool.App.Domain;

namespace DanceSchool.Tests;

/// <summary>Test double made possible by DIP: services never know it is not a real channel.</summary>
public class FakeNotificationSender : INotificationSender
{
    public List<(Student Recipient, string Message)> Sent { get; } = [];

    public void Send(Student recipient, string message) => Sent.Add((recipient, message));
}
