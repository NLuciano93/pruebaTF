using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.Users.V1.Get
{
    public class ShowAllAdminsHandler : Handler<ShowAllAdminsRequest, IPagination<RegistrationSummaryResponse>>
    {
        private readonly ILogger<ShowAllAdminsHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public ShowAllAdminsHandler(ILogger<ShowAllAdminsHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<IPagination<RegistrationSummaryResponse>>> Handle(ShowAllAdminsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var showAllUsersDto = _mapper.Map<ShowAllUsersDto>(request);

                showAllUsersDto.RoleId = (int)EnabledRoles.Leader;

                if (string.IsNullOrEmpty(showAllUsersDto.SortColumn))
                {
                    showAllUsersDto.SortColumn = "Name";
                }

                if (string.IsNullOrEmpty(showAllUsersDto.SortDirection))
                {
                    showAllUsersDto.SortDirection = "ASC";
                }

                var result = await _service.GetAllUsersAsync(showAllUsersDto);

                return Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
