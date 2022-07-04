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
    public class GetUsersHoursNoCompleteHandler : Handler<GetUsersHoursNoCompleteRequest, IPagination<UsersHoursNoCompleteResponse>>
    {
        private readonly ILogger<GetUsersHoursNoCompleteHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public GetUsersHoursNoCompleteHandler(ILogger<GetUsersHoursNoCompleteHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<IPagination<UsersHoursNoCompleteResponse>>> Handle(GetUsersHoursNoCompleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var usersHoursNoCompleteDto = _mapper.Map<UsersHoursNoCompleteDto>(request);

                if (string.IsNullOrEmpty(usersHoursNoCompleteDto.SortColumn))
                {
                    usersHoursNoCompleteDto.SortColumn = "Name";
                }

                if (string.IsNullOrEmpty(usersHoursNoCompleteDto.SortDirection))
                {
                    usersHoursNoCompleteDto.SortDirection = "ASC";
                }

                var result = await _service.GetUsersHoursNoCompleteAsync(usersHoursNoCompleteDto);

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
