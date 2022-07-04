using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fusap.Common.Authorization.Client;
using Fusap.Common.Authorization.WebApi;

namespace Sample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IFusapAuthorizationClient _FusapAuthorizationClient;

        public TestController(IFusapAuthorizationClient FusapAuthorizationClient)
        {
            _FusapAuthorizationClient = FusapAuthorizationClient;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<TestResponse>> TestAsync()
        {
            var personIdentity = FusapResources.Person(User.GetSubjectGuid());
            var account1Resource = FusapResources.Account(Guid.NewGuid());
            var account2Resource = FusapResources.Account(Guid.NewGuid());

            var canViewBothBalances = new Requirement(
                personIdentity,
                new[] { account1Resource, account2Resource },
                "account:view-balance"
            );
            var canTransferFromAcc1 = new Requirement(
                personIdentity,
                account1Resource,
                "account:transfer-funds", "account:view-transfer-receipts"
            );

            // You can query any number of requirements as long as all resources can be authorized by a single ResourceAuthority
            var requirements = new[] { canViewBothBalances, canTransferFromAcc1 };

            var authorizationResult = await _FusapAuthorizationClient.AuthorizeAsync(requirements);

            if (authorizationResult)
            {
                // All grants successful
            }
            if (authorizationResult[canViewBothBalances])
            {
                // First grant successful
            }
            if (authorizationResult[canTransferFromAcc1])
            {
                // Second grant successful
            }

            var response = new TestResponse
            {
                AllGrants = authorizationResult,
                CanViewBothBalances = authorizationResult[canViewBothBalances],
                CanTransferFromAcc1 = authorizationResult[canTransferFromAcc1]
            };

            if (await _FusapAuthorizationClient.AuthorizeAsync(personIdentity, account1Resource, "account:associate-card"))
            {
                // User logged in as Person can associate card to account 1.

                response.CanAssociateCard = true;
            }

            if (await _FusapAuthorizationClient.AuthorizeAsync(personIdentity, new[] { account1Resource, account2Resource },
                new[] { "payroll:upload-batch-transaction", "payroll:upload-batch-associate" }))
            {
                // User logged in as Person can upload batch transactions and batch associates to accounts 1 and 2.

                response.CanBatchUpload = true;
            }

            return Ok(response);
        }
    }

    public class TestResponse
    {
        public bool AllGrants { get; set; }
        public bool CanViewBothBalances { get; set; }
        public bool CanTransferFromAcc1 { get; set; }
        public bool CanAssociateCard { get; set; }
        public bool CanBatchUpload { get; set; }
    }
}
