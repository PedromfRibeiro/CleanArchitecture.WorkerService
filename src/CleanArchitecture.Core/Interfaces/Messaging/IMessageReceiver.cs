namespace CleanArchitecture.Core.Interfaces.Messaging;

public interface IMessageReceiver
{
    Task ReceiveMessageAsync(string message);
}
