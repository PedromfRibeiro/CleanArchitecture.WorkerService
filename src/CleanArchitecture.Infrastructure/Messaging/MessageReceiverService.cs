using CleanArchitecture.Core.Interfaces.Messaging;

namespace CleanArchitecture.Infrastructure.Messaging;

public class MessageReceiverService : IMessageReceiver
{
    public async Task ReceiveMessageAsync(string message)
    {
        await Task.Delay(0);
        Console.WriteLine($"Received message: {message}");
    }
}
