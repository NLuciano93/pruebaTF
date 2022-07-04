using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fusap.Common.Model.Tests.Errors
{
    public class BusinessRuleViolationErrorTests
    {
        [Fact]
        public void Construtor_WithDetails_ProducesError()
        {
            // Arrange
            var details = new[]
            {
                new BusinessRuleViolation("code1", "message1", new Dictionary<string, string>
                {
                    { "prop1", "value1" },
                    { "prop2", "value2" },
                }),
                new BusinessRuleViolation(ErrorCatalog.ErrorWithZeroParameters, new Dictionary<string, string>
                {
                    { "prop3", "value3" },
                }),
                new BusinessRuleViolation(ErrorCatalog.Test),
                ErrorCatalog.ErrorWithOneParameter
            };

            // Act
            var error = new BusinessRuleViolatedError(details);

            // Assert
            Assert.Contains(error.Violations, x => x.Code == "code1" &&
                x.Properties!.Any(y => y.Key == "prop1") && x.Properties!.Any(y => y.Key == "prop2"));
            Assert.Contains(error.Violations, x => x.Code == ErrorCatalog.ErrorWithZeroParameters &&
                x.Properties!.Any(y => y.Key == "prop3"));
            Assert.Contains(error.Violations, x => x.Code == ErrorCatalog.Test && x.Properties == null);
            Assert.Contains(error.Violations, x => x.Code == ErrorCatalog.ErrorWithOneParameter && x.Properties == null);
        }

        [Fact]
        public void ToString_WithDetails_ProducesCorrectRepresentation()
        {
            // Arrange
            var details = new[]
            {
                new BusinessRuleViolation("code1", "message1", new Dictionary<string, string>
                {
                    { "prop1", "value1" },
                    { "prop2", "value2" },
                }),
                new BusinessRuleViolation(ErrorCatalog.ErrorWithZeroParameters, new Dictionary<string, string>
                {
                    { "prop3", "value3" },
                }),
                new BusinessRuleViolation(ErrorCatalog.Test),
                ErrorCatalog.ErrorWithOneParameter
            };
            var error = new BusinessRuleViolatedError(details);

            // Act
            var str = error.ToString();

            // Assert
            Assert.Equal("BUSINESS_RULE_VIOLATION: One or more business rules were violated (code1 - message1 [prop1=value1;prop2=value2], " +
                         "RN-01 - This error has a static message [prop3=value3], " +
                         "RN-06 - This error has four parameter and they are '{0}', '{1}', '{2}' and '{3}', " +
                         "RN-02 - This error has one parameter and it is '{0}')", str);
        }

        [Fact]
        public void ToString_WithNoDetails_ProducesCorrectRepresentation()
        {
            // Arrange
            var error = new BusinessRuleViolatedError();

            // Act
            var str = error.ToString();

            // Assert
            Assert.Equal("BUSINESS_RULE_VIOLATION: One or more business rules were violated (no details)", str);
        }
    }
}
