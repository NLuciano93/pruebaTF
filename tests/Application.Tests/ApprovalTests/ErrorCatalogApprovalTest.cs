using ApprovalTests;
using ApprovalTests.Reporters;
using Fusap.Common.Model.ErrorCatalogs;
using Fusap.TimeSheet.Application.Notifications;
using Xunit;

namespace Fusap.TimeSheet.Application.Tests.ApprovalTests
{
    [UseReporter(typeof(ClipboardReporter), typeof(DiffReporter))]
    public sealed class ErrorCatalogApprovalTests
    {
        [Fact]
        public void ErrorCatalog()
        {
            //Arrange

            //Act
            var catalogDescription = ErrorCatalogDescription.For(typeof(ErrorCatalog)).SerializeAsMarkdown();

            //Assert
            Approvals.Verify(catalogDescription);
        }
    }
}
