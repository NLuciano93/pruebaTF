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
    public class ShowAllUsersUnderAdminHandler : Handler<ShowAllUsersUnderAdminRequest, IPagination<RegistrationSummaryResponse>>
    {
        private readonly ILogger<ShowAllUsersUnderAdminHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public ShowAllUsersUnderAdminHandler(ILogger<ShowAllUsersUnderAdminHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<IPagination<RegistrationSummaryResponse>>> Handle(ShowAllUsersUnderAdminRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var showAllUsersUnderAdminDto = _mapper.Map<ShowAllUsersUnderAdminDto>(request);

                if (string.IsNullOrEmpty(showAllUsersUnderAdminDto.SortColumn))
                {
                    showAllUsersUnderAdminDto.SortColumn = "Name";
                }

                if (string.IsNullOrEmpty(showAllUsersUnderAdminDto.SortDirection))
                {
                    showAllUsersUnderAdminDto.SortDirection = "ASC";
                }

                var result = await _service.GetAllUsersUnderAdminAsync(showAllUsersUnderAdminDto);

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
