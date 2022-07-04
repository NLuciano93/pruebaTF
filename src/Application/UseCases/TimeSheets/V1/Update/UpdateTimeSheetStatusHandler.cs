﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Microsoft.Extensions.Logging;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheets.V1.Update
{
    public class UpdateTimeSheetStatusHandler : Handler<UpdateTimeSheetStatusRequest>
    {
        private readonly ILogger<UpdateTimeSheetStatusHandler> _logger;
        private readonly IService _service;

        public UpdateTimeSheetStatusHandler(ILogger<UpdateTimeSheetStatusHandler> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result> Handle(UpdateTimeSheetStatusRequest request, CancellationToken cancellationToken)
        {
            var validations = new UpdateTimeSheetStatusValidator();

            var results = validations.Validate(request);

            if (!results.IsValid)
            {
                var validationErrorDetail = new List<ValidationErrorDetail>();

                foreach (var failure in results.Errors)
                {
                    validationErrorDetail.Add(new ValidationErrorDetail(failure.PropertyName, failure.ErrorCode, failure.ErrorMessage));
                }

                return Invalid(validationErrorDetail.ToArray());
            }

            try
            {
                await _service.UpdateTimeSheetStatusAsync(request.TimeSheetMasterId, request.TimeSheetStatus, request.Comment);

                return Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }
    }
}
