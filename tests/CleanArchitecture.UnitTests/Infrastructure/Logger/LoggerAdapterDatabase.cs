using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repository;
using CleanArchitecture.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchitecture.UnitTests.Infrastructure.Logger;

internal class LoggerAdapterDatabase
{
    private readonly Mock<ILogger<LoggerAdapterTester>> _mockLogger;

    private readonly Mock<IRepositoryAsync<LogEntry>> _mockRepo;

    public LoggerAdapterDatabase()
    {
        _mockLogger = new Mock<ILogger<LoggerAdapterTester>>();
        _mockRepo = new Mock<IRepositoryAsync<LogEntry>>();
    }

    [Test]
    public void Validate_Database_Entry_Inserted()
    {
        //Arrange
        LoggerAdapter<LoggerAdapterTester> someService = new(_mockLogger.Object, _mockRepo.Object);

        //Act
        //Assert
    }
}
