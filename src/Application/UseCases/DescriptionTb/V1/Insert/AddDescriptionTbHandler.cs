using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.DescriptionTb.V1.Insert
{
    public class AddDescriptionTbHandler : Handler<AddDescriptionTbRequest, DescriptionTbIdResponse>
    {
        private readonly ILogger<AddDescriptionTbHandler> _logger;
        private readonly IService _service;
        private readonly IMapper _mapper;

        public AddDescriptionTbHandler(ILogger<AddDescriptionTbHandler> logger, IService service, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<Result<DescriptionTbIdResponse>> Handle(AddDescriptionTbRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var descriptionTb = _mapper.Map<Domain.TimeSheetAggregate.Entities.DescriptionTb>(request);
                descriptionTb.CreatedOn = DateTime.Now;

                var descriptionId = await _service.InsertAsync(descriptionTb: descriptionTb);

                return new DescriptionTbIdResponse() { DescriptionId = descriptionId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
