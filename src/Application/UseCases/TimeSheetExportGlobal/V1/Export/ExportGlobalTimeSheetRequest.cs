using System;
using Fusap.Common.Mediator;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetExportGlobal.V1.Export
{
    public class ExportGlobalTimeSheetRequest : Request<ExportGlobalTimeSheetResponse>
    {
        public DateTime Period { get; set; }
    }
}
