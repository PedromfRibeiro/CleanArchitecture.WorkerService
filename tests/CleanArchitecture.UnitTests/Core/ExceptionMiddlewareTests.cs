using System.Net;
using System.Text.Json;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Entities.Errors;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repository;
using CleanArchitecture.Core.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;

namespace CleanArchitecture.UnitTests.Core;

public class ExceptionMiddlewareTests
{
    private Mock<IHostEnvironment> _envMock;

    private HttpContext _httpContext;

    private Mock<ILogger<ExceptionMiddleware>> _loggerMock;

    private ExceptionMiddleware _middleware;

    private Mock<RequestDelegate> _nextMock;

    private Mock<IRepositoryAsync> _repositoryMock;

    private Mock<IServiceLocator> _serviceLocatorMock;

    private Mock<IServiceProvider> _serviceProviderMock;

    private Mock<IServiceScope> _serviceScopeMock;

    public ExceptionMiddlewareTests()
    {
        _nextMock = new Mock<RequestDelegate>();
        _loggerMock = new Mock<ILogger<ExceptionMiddleware>>();
        _envMock = new Mock<IHostEnvironment>();
        _serviceLocatorMock = new Mock<IServiceLocator>();
        _serviceScopeMock = new Mock<IServiceScope>();
        _serviceProviderMock = new Mock<IServiceProvider>();
        _repositoryMock = new Mock<IRepositoryAsync>();

        _serviceLocatorMock.Setup(s => s.CreateScope()).Returns(_serviceScopeMock.Object);
        _serviceScopeMock.Setup(s => s.ServiceProvider).Returns(_serviceProviderMock.Object);
        _serviceProviderMock.Setup(sp => sp.GetService(typeof(IRepositoryAsync))).Returns(_repositoryMock.Object);

        // Ensure EnvironmentName is explicitly set
        _envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production);

        _httpContext = new DefaultHttpContext();
        _middleware = new ExceptionMiddleware(_nextMock.Object, _loggerMock.Object, _envMock.Object, _serviceLocatorMock.Object);
    }

    [Test]
    public async Task ShouldCallNext_WhenNoExceptionOccurs()
    {
        // Arrange
        _nextMock.Setup(n => n(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

        // Act
        await _middleware.InvokeAsync(_httpContext);

        // Assert
        _nextMock.Verify(n => n(It.IsAny<HttpContext>()), Times.Once);
    }

    [Test]
    public async Task ShouldDefaultLogError_WhenInProductionMode()
    {
        // Arrange
        string contentMessage = "Test logging exception";
        var exception = new Exception(contentMessage);
        _nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(exception);
        _envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production);

        // Act
        await _middleware.InvokeAsync(_httpContext);

        // Assert
        await Assert.That(contentMessage).IsNotEqualTo(_httpContext.Response.Body.ToString());
    }

    [Test]
    public async Task ShouldHandleException_AndReturnInternalServerError()
    {
        // Arrange
        var exception = new Exception("Test exception");
        _nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(exception);
        _envMock.Setup(e => e.EnvironmentName).Returns(Environments.Development);

        // Act
        await _middleware.InvokeAsync(_httpContext);

        // Assert
        Assert.Equals((int)HttpStatusCode.InternalServerError, _httpContext.Response.StatusCode);
    }

    [Test]
    public async Task ShouldLogError_WhenExceptionOccurs()
    {
        // Arrange
        string contentMessage = "Test logging exception";
        var exception = new Exception(contentMessage);
        _nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(exception);
        _envMock.Setup(e => e.EnvironmentName).Returns(Environments.Production); // Make sure to set this to test non-dev behavior

        // Capture response body
        using var responseStream = new MemoryStream();
        _httpContext.Response.Body = responseStream;

        // Act
        await _middleware.InvokeAsync(_httpContext);

        // Assert
        responseStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(responseStream);
        string responseBody = await reader.ReadToEndAsync();

        ApiException apiException = new(500, HttpStatusCode.InternalServerError.ToString(), string.Empty);
        string json = JsonSerializer.Serialize(apiException);
        Assert.Equals(json, responseBody);
    }

    [Test]
    public async Task ShouldSaveErrorToDatabase_WhenInDevelopmentMode()
    {
        // Arrange
        var exception = new Exception("Test database log exception");
        _nextMock.Setup(n => n(It.IsAny<HttpContext>())).ThrowsAsync(exception);
        _envMock.Setup(e => e.EnvironmentName).Returns(Environments.Development);

        // Act
        await _middleware.InvokeAsync(_httpContext);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<LogEntry>()), Times.Once);
    }
}
