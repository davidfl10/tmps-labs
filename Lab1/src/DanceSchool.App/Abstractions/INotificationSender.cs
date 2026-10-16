using DanceSchool.App.Domain;

namespace DanceSchool.App.Abstractions;

public interface INotificationSender
{
    void Send(Student recipient, string message);
}
