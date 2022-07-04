using System.Threading.Tasks;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.UseCases.Users.V1.Get;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Input;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.TimeSheet.Api.Controller.Users.V1
{
    public partial class UsersController
    {
        /// <summary>
        /// Show all Users
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet("all-users")]
        public async Task<ActionResult<Pagination<RegistrationSummaryResponse>>> ShowAllUsersAsync([FromQuery] ShowAllUsersInput input)
        {
            var request = Map<ShowAllUsersRequest>(input);

            var result = await Send(request);

            return OkAs<Pagination<RegistrationSummaryResponse>>(result);
        }

        /// <summary>
        /// Show all Admins
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet("all-admins")]
        public async Task<ActionResult<Pagination<RegistrationSummaryResponse>>> ShowAllAdminsAsync([FromQuery] ShowAllAdminsInput input)
        {
            var request = Map<ShowAllAdminsRequest>(input);

            var result = await Send(request);

            return OkAs<Pagination<RegistrationSummaryResponse>>(result);
        }

        /// <summary>
        /// Get Users hours no complete
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet("users-hours-no-complete")]
        public async Task<ActionResult<Pagination<UsersHoursNoCompleteResponse>>> GetUsersHoursNoCompleteAsync([FromQuery] GetUsersHoursNoCompleteInput input)
        {
            var request = Map<GetUsersHoursNoCompleteRequest>(input);

            var result = await Send(request);

            return OkAs<Pagination<UsersHoursNoCompleteResponse>>(result);
        }

        /// <summary>
        /// Get User details by RegistrationId
        /// </summary>
        /// <param name="registrationId">RegistrationId</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("{registrationId}/user")]
        public async Task<ActionResult<RegistrationViewDetailsResponse>> GetUserDetailsByRegistrationIdAsync([FromRoute] int registrationId)
        {
            var result = await Send(new GetUserDetailsByRegistrationIdRequest(registrationId));

            return OkAs<RegistrationViewDetailsResponse>(result);
        }

        /// <summary>
        /// Get Admin details by RegistrationId
        /// </summary>
        /// <param name="registrationId">RegistrationId</param>
        /// <returns></returns>
        /// <response code="404">
        /// Not Found: indicates that the server can't find the requested resource.
        /// </response>
        [HttpGet("{registrationId}/admin")]
        public async Task<ActionResult<RegistrationViewDetailsResponse>> GetAdminDetailsByRegistrationIdAsync([FromRoute] int registrationId)
        {
            var result = await Send(new GetAdminDetailsByRegistrationIdRequest(registrationId));

            return OkAs<RegistrationViewDetailsResponse>(result);
        }

        /// <summary>
        /// Get Total Admins Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("total-admins-count")]
        public async Task<ActionResult<int>> GetTotalAdminsCountAsync()
        {
            var result = await Send(new GetTotalAdminsCountRequest());

            return OkAs<int>(result);
        }

        /// <summary>
        /// Get Total Users Count
        /// </summary>
        /// <returns></returns>
        [HttpGet("total-users-count")]
        public async Task<ActionResult<int>> GetTotalUsersCountAsync()
        {
            var result = await Send(new GetTotalUsersCountRequest());

            return OkAs<int>(result);
        }

        /// <summary>
        /// Show all Users under Admin
        /// </summary>
        /// <param name="input">Request for query input</param>
        /// <returns></returns>
        [HttpGet("users-under-admin")]
        public async Task<ActionResult<Pagination<RegistrationSummaryResponse>>> ShowAllUsersUnderAdminAsync([FromQuery] SearchAllUsersUnderAdminsInput input)
        {
            var request = Map<ShowAllUsersUnderAdminRequest>(input);

            request.AdminUserId = GetUser();

            var result = await Send(request);

            return OkAs<Pagination<RegistrationSummaryResponse>>(result);
        }
    }
}
