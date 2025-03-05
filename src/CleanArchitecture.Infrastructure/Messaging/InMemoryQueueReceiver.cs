using CleanArchitecture.Core.Interfaces.Messaging;

namespace CleanArchitecture.Infrastructure.Messaging;

/// <summary>
/// A simple implementation using the built-in Queue type and a single static instance.
/// </summary>
public class InMemoryQueueReceiver : IQueueReceiver
{
    public static Queue<string> MessageQueue = new Queue<string>();

    public async Task<string> GetMessageFromQueue(string queueName)
    {
        await Task.CompletedTask; // just so async is allowed
        if (MessageQueue.Count == 0) return string.Empty;
        return MessageQueue.Dequeue();
    }
}
