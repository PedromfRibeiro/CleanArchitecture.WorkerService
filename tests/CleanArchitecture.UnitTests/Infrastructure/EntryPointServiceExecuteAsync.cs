using CleanArchitecture.Infrastructure.Messaging;

namespace CleanArchitecture.UnitTests.Infrastructure;

public class InMemoryQueueReceiverGetMessageFromQueue
{
    private readonly InMemoryQueueReceiver _receiver = new InMemoryQueueReceiver();

    [Test]
    public async Task ThrowsArgumentExceptionGivenEmptyQueuename()
    {
        var ex = await Assert.ThrowsAsync<ArgumentException>(() => _receiver.GetMessageFromQueue(String.Empty));
    }

    [Test]
    public async Task ThrowsNullExceptionGivenNullQueuename()
    {
        var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _receiver.GetMessageFromQueue(string.Empty));
    }
}
