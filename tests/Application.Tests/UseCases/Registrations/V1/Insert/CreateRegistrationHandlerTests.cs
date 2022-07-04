using AutoMapper;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert.Tests
{
    public class CreateRegistrationHandlerTests
    {
        [Fact]
        public void CreateRegistrationHandlerConstructorOk()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<AddRegistrationHandler>>();
            var mockService = new Mock<IService>();
            var mockMapper = new Mock<IMapper>();

            // Act
            var createRegistrationHandler = new AddRegistrationHandler(mockLogger.Object, mockService.Object, mockMapper.Object);

            // Assert
            Assert.NotNull(createRegistrationHandler);
            Assert.IsType<AddRegistrationHandler>(createRegistrationHandler);
        }
    }
}
