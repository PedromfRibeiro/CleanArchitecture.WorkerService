using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repository;
using CleanArchitecture.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchitecture.UnitTests.Infrastructure.Logger;

public class LoggerAdapterTester
{
    private Mock<ILogger<LoggerAdapterTester>> _mockLogger;

    private Mock<IRepositoryAsync> _mockRepo;

    public LoggerAdapterTester()
    {
        _mockLogger = new Mock<ILogger<LoggerAdapterTester>>();
        _mockRepo = new Mock<IRepositoryAsync>();
    }

    [Test]
    public void LogLevel_Debug()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);
        string MessageValue = "Test Message";

        //Act
        someService.LogDebug(new Exception(), MessageValue);
        //Assert
        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Debug,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == MessageValue),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once);
    }

    [Test]
    public void LogLevel_Error()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);
        string MessageValue = "Test Message";

        //Act
        someService.LogError(new Exception(), MessageValue);
        //Assert
        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == MessageValue),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once);
    }

    [Test]
    public void LogLevel_Generic()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);
        string MessageValue = "Test Message";
        LogLevel level = LogLevel.Warning;

        //Act
        someService.Log(level, new Exception(), MessageValue);

        //Assert
        _mockLogger.Verify(
            l => l.Log(
                level,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == MessageValue),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once);
    }

    [Test]
    public void LogLevel_Information()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);
        string MessageValue = "Test Message";

        //Act
        someService.LogInformation(MessageValue);
        //Assert
        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == MessageValue),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once);
    }

    [Test]
    public void LogLevel_Queue()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);
        string MessageValue = "MessageValue";
        string statusValue = "Test status";
        string operationTypeValue = "Test operation Type";
        string expectedLogMessage = string.Format("Status:{0} | Operation:{1} | " + MessageValue, statusValue, operationTypeValue);
        LogLevel Level = LogLevel.Warning;

        //Act
        someService.LogQueue(Level, operationTypeValue, statusValue, MessageValue, new Exception());
        //Assert
        _mockLogger.Verify(
         l => l.Log(
             Level,
             It.IsAny<EventId>(),
             It.Is<It.IsAnyType>((v, t) => v.ToString() == expectedLogMessage),
             It.IsAny<Exception>(),
             It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
         Times.Once);
    }

    [Test]
    public void LogLevel_Warning()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);
        string MessageValue = "Test Message";

        //Act
        someService.LogWarning(new Exception(), MessageValue);
        //Assert
        _mockLogger.Verify(
            l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == MessageValue),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()),
            Times.Once);
    }
}
