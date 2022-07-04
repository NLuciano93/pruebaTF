using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using Fusap.Common.Model.ErrorCatalogs;
using Xunit;

namespace Fusap.Common.Model.Tests.ErrorCatalogs
{
    [UseReporter(typeof(ClipboardReporter), typeof(DiffReporter))]
    public class ErrorCatalogDescriptionTests
    {
        [Fact]
        public void For_WithValidCatalog_ProducesValidDescription()
        {
            // Arrange

            // Act
            var catalog = ErrorCatalogDescription.For(typeof(ErrorCatalog));

            // Assert
            var markdown = catalog.SerializeAsMarkdown();
            Approvals.VerifyWithExtension(markdown, ".md");
        }

        [Fact]
        public void For_WithDuplicatedCodes_Throws()
        {
            // Arrange

            // Act
            void Act()
            {
                ErrorCatalogDescription.For(typeof(ErrorCatalogWithDuplicatedCodes));
            }

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Equal("Error codes duplicated in catalog: RN-01", exception.Message);
        }

        [Fact]
        public void For_WithInvalidProperties_Throws()
        {
            // Arrange

            // Act
            void Act()
            {
                ErrorCatalogDescription.For(typeof(ErrorCatalogWithInvalidProperties));
            }

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Equal("All properties in the error catalog must be static, have only a getter and have the return type of " +
                         "`ErrorCatalogEntry`. These properties violate this rule: Fusap.Common.Model.Tests.ErrorCatalogs." +
                         "ErrorCatalogWithInvalidProperties.EntryOne, Fusap.Common.Model.Tests.ErrorCatalogs.ErrorCatalogWithInvalidProperties" +
                         ".EntryTwo, Fusap.Common.Model.Tests.ErrorCatalogs.ErrorCatalogWithInvalidProperties.EntryThree", exception.Message);
        }

        [Fact]
        public void For_WithFields_Throws()
        {
            // Arrange

            // Act
            void Act()
            {
                ErrorCatalogDescription.For(typeof(ErrorCatalogWithFields));
            }

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Equal("You should not store error catalog entries as fields. Please convert these to properties: " +
                         "Fusap.Common.Model.Tests.ErrorCatalogs.ErrorCatalogWithFields.EntryOne, Fusap.Common.Model.Tests." +
                         "ErrorCatalogs.ErrorCatalogWithFields.EntryTwo", exception.Message);
        }

        [Fact]
        public void For_WithMissingCodeAndMessage_Throws()
        {
            // Arrange

            // Act
            void Act()
            {
                ErrorCatalogDescription.For(typeof(ErrorCatalogWithMissingCodeAndMessage));
            }

            // Assert
            var exception = Assert.Throws<ArgumentException>(Act);
            Assert.Equal("Catalog entries have missing error codes or messages: Fusap.Common.Model.Tests.ErrorCatalogs" +
                         ".ErrorCatalogWithMissingCodeAndMessage.EntryOne, Fusap.Common.Model.Tests.ErrorCatalogs." +
                         "ErrorCatalogWithMissingCodeAndMessage.EntryTwo", exception.Message);
        }
    }
}
