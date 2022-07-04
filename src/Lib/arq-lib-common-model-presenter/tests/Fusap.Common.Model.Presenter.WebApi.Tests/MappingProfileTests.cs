using AutoMapper;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingProfilesHaveValidConfiguration()
        {
            // Arrange
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DefaultMappingProfile).Assembly));

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }
    }
}
