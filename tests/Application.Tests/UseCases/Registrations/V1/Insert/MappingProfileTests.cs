using AutoMapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Xunit;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert.Tests
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingHaveValidConfiguration()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddMaps(new[] {
                typeof(AddRegistrationRequest).Assembly,
                typeof(Registration).Assembly}));

            // Assert
            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void MappingProfilesHaveValidConfiguration()
        {
            // Arrange
            var config = new MappingProfile();

            // Assert
            Assert.Equal("Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert.MappingProfile", config.ProfileName);
        }
    }
}
