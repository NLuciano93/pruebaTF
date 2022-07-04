using ApprovalTests;
using ApprovalTests.Reporters;
using Fusap.Common.Model.ErrorCatalogs;
using Xunit;

namespace Fusap.Common.Model.Presenter.WebApi.Tests.ApprovalTests
{
    [UseReporter(typeof(ClipboardReporter), typeof(DiffReporter))]
    public sealed class ErrorCatalogApprovalTests
    {
        [Fact]
        public void ErrorCatalog()
        {
            // Arrange

            // Act
            var catalogDescription = ErrorCatalogDescription.For(typeof(RootErrorCatalog)).SerializeAsMarkdown();

            // Assert
            Approvals.Verify(catalogDescription);
        }
    }
}
