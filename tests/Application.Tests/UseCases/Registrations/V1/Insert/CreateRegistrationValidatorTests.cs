using System;
using FluentValidation.TestHelper;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Xunit;

namespace Fusap.TimeSheet.Application.UseCases.Registrations.V1.Insert.Tests
{
    public class CreateRegistrationValidatorTests
    {
        [Fact]
        public void Validate_WithValidFields_ReturnsNoValidationErrors()
        {
            // Arrange
            var createRegistrationRequest = new AddRegistrationRequest()
            {
                Name = "Oscar Ariel",
                Mobileno = "9998887770",
                EmailId = "test@hotmail.com",
                Username = "amenapace",
                Password = "1234567",
                ConfirmPassword = "1234567",
                Gender = "M",
                Birthdate = DateTime.Parse("2022-03-22T11:15:01.413Z"),
                Roles = EnabledRoles.User
            };

            var sut = new AddRegistrationValidator();

            // Act
            var result = sut.TestValidate(createRegistrationRequest);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData("test test@mail.com")]
        [InlineData("test.mail.com")]
        [InlineData("test@mail")]
        [InlineData(" ")]
        [InlineData("")]
        public void Validate_WithInvalidEmailId_ReturnsValidationErrors(string emailId)
        {
            // Arrange
            var createRegistrationRequest = new AddRegistrationRequest()
            {
                Name = "Oscar Ariel",
                Mobileno = "9998887770",
                EmailId = emailId,
                Username = "amenapace",
                Password = "1234567",
                ConfirmPassword = "1234567",
                Gender = "M",
                Birthdate = DateTime.Parse("2022-03-22T11:15:01.413Z"),
                Roles = EnabledRoles.User
            };

            var sut = new AddRegistrationValidator();

            // Act
            var result = sut.TestValidate(createRegistrationRequest);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.EmailId)
                .WithErrorCode(ErrorCatalog.TimeSheet.InvalidEmailId);
        }

        [Fact]
        public void Validate_EmailIdIsNull_ReturnsValidationErrors()
        {
            // Arrange
            var createRegistrationRequest = new AddRegistrationRequest()
            {
                Name = "Oscar Ariel",
                Mobileno = "9998887770",
                EmailId = null,
                Username = "amenapace",
                Password = "1234567",
                ConfirmPassword = "1234567",
                Gender = "M",
                Birthdate = DateTime.Parse("2022-03-22T11:15:01.413Z"),
                Roles = EnabledRoles.User
            };

            var sut = new AddRegistrationValidator();

            // Act
            var result = sut.TestValidate(createRegistrationRequest);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.EmailId)
                .WithErrorCode(ErrorCatalog.TimeSheet.UndefinedEmailId);
        }

        [Fact]
        public void Validate_PasswordsDidNotMatch_ReturnsValidationErrors()
        {
            // Arrange
            var createRegistrationRequest = new AddRegistrationRequest()
            {
                Name = "Oscar Ariel",
                Mobileno = "9998887770",
                EmailId = "test@hotmail.com",
                Username = "amenapace",
                Password = "1234567",
                ConfirmPassword = "1234568",
                Gender = "M",
                Birthdate = DateTime.Parse("2022-03-22T11:15:01.413Z"),
                Roles = EnabledRoles.User
            };

            var sut = new AddRegistrationValidator();

            // Act
            var result = sut.TestValidate(createRegistrationRequest);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ConfirmPassword)
                .WithErrorCode(ErrorCatalog.TimeSheet.PasswordsDidNotMatch);
        }
    }
}
