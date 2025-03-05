namespace CleanArchitecture.Core.Interfaces.Messaging;

public interface IQueueSender
{
    Task SendMessageToQueue(string message, string queueName);
}
