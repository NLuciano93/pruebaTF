using System;
using Xunit;

namespace Fusap.Common.Mediator.Tests
{
    public class FusapAppDomainExtensionsTests
    {
        [Fact]
        public void GetFusapLoadedAssemblies_WithAppDomain_ReturnsLoadedAssemblies()
        {
            // Arrange
            var domain = AppDomain.CurrentDomain;
            domain.Load("Fusap.Common.Mediator");

            // Act
            var assemblies = domain.GetFusapLoadedAssemblies();

            // Assert
            Assert.Contains(assemblies, x => x.GetName().Name == "Fusap.Common.Mediator");
        }
    }
}
