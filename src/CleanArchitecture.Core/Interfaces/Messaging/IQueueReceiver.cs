namespace CleanArchitecture.Core.Interfaces.Messaging;

public interface IQueueReceiver
{
    Task<string> GetMessageFromQueue(string queueName);
}
