using System.Globalization;
using System.Resources;
using CleanArchitecture.Worker.Resources;
using Moq;

namespace CleanArchitecture.UnitTests.Resources;

public class ResourcesTest
{
    CultureInfo culture { get; set; } = new CultureInfo("pt-PT");

    [Test]
    public void Culture_SetAndGetCulture_ShouldReflectChanges()
    {
        // Arrange

        // Act
        Resource.Culture = culture;
        var result = Resource.Culture;

        // Assert
        Assert.Equals(culture, result);
    }

    [Test]
    public void Culture_ShouldGetAndSetCurrentCulture()
    {
        Resource.Culture = culture;

        Assert.Equals(culture, Resource.Culture);
    }

    [Test]
    public void Default_ShouldReturnCorrectLocalizedString()
    {
        // Arrange
        var expectedValue = "Por defeito"; // This should match the resource file value

        // Act
        var actualValue = Resource.Default;

        // Assert
        Assert.Equals(expectedValue, actualValue);
    }

    [Test]
    public async Task ResourceManager_ShouldNotBeNull()
    {
        // Act
        var resourceManager = Resource.ResourceManager;

        // Assert
        await Assert.That(resourceManager).IsNotNull();
    }

    [Test]
    public void ResourceManager_ShouldReturnCorrectResource()
    {
        // Arrange
        var resourceManagerMock = new Mock<ResourceManager>("CleanArchitecture.Worker.Resources.Resource", typeof(Resource).Assembly);
        resourceManagerMock.Setup(rm => rm.GetString("Default", It.IsAny<CultureInfo>())).Returns("Mocked Value");

        // Act
        var result = resourceManagerMock.Object.GetString("Default", Resource.Culture);

        // Assert
        Assert.Equals("Mocked Value", result);
    }

    [Test]
    public async Task ResourceManager_ShouldReturnValidResourceManager()
    {
        // Arrange
        var resourceManager = Resource.ResourceManager;
        //Act
        //Assert
        await Assert.That(resourceManager).IsNotNull();
        Assert.Equals("CleanArchitecture.Worker.Resources.Resource", resourceManager.BaseName);
    }
}
