using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fusap.Common.Mediator;
using Fusap.Common.Model;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Data;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fusap.TimeSheet.Application.UseCases.TimeSheetExportGlobal.V1.Export
{
    public class ExportGlobalTimeSheetHandler : Handler<ExportGlobalTimeSheetRequest, ExportGlobalTimeSheetResponse>
    {
        private readonly ILogger<ExportGlobalTimeSheetRequest> _logger;
        private readonly IService _service;

        public ExportGlobalTimeSheetHandler(ILogger<ExportGlobalTimeSheetRequest> logger, IService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override async Task<Result<ExportGlobalTimeSheetResponse>> Handle(ExportGlobalTimeSheetRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var timesheetdata = await _service.GetTimeSheetMasterIDByPeriodAsync(request.Period);

                var isValid = ValidateData(timesheetdata);

                if (isValid)
                {
                    var periodSelected = request.Period;
                    var dt = new DataTable();

                    var codeProjects = await _service.GetAllCodeProjectsAsync();
                    var users = await _service.GetAllUsersAsync();

                    dt.Columns.Add("EmployeeID", typeof(string));
                    dt.Columns.Add("Name", typeof(string));

                    foreach (var codeProject in codeProjects)
                    {
                        dt.Columns.Add(codeProject.ProjectCode, typeof(string));
                    }

                    foreach (var user in users)
                    {
                        var row = dt.NewRow();
                        row["EmployeeID"] = user.EmployeeID;
                        row["Name"] = user.Name;

                        dt.Rows.Add(row);
                    }

                    foreach (var item in timesheetdata)
                    {
                        var timesheetId = Convert.ToInt32(item.TimeSheetMasterID);
                        var timesheetDetails = await _service.TimesheetDetailsbyTimeSheetMasterIdAsync(timesheetId);

                        foreach (var detail in timesheetDetails)
                        {
                            if (detail != null)
                            {
                                DateTime period = Convert.ToDateTime(detail.Period);

                                if (period.Month == periodSelected.Month)
                                {
                                    var project = await _service.GetProjectCodeByIdAsync(detail.ProjectID);
                                    var user = await _service.GetUserByIdAsync(detail.UserID);

                                    var cell = dt.Select("Name = '" + user + "'");

                                    if (cell.Length > 0)
                                    {
                                        int index = dt.Rows.IndexOf(cell[0]);
                                        var value = dt.Rows[index][project].ToString();

                                        if (!string.IsNullOrEmpty(value))
                                        {
                                            decimal hours = Convert.ToDecimal(value) + Convert.ToDecimal(detail.Hours);
                                            dt.Rows[index][project] = hours;
                                        }
                                        else
                                        {
                                            dt.Rows[index][project] = detail.Hours;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    var response = new ExportGlobalTimeSheetResponse();
                    var jsonString = JsonConvert.SerializeObject(dt) ?? string.Empty;

#pragma warning disable CS8601 // Possible null reference assignment.
                    response.GlobalTimesheetList = JsonConvert.DeserializeObject(jsonString);
#pragma warning restore CS8601 // Possible null reference assignment.

                    return response;
                }

                return new ExportGlobalTimeSheetResponse() { GlobalTimesheetList = "There is no data to export in the selected period" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Source: {source} - Message: {message}", ex.Source, ex.Message);
                return Error(ErrorCatalog.TimeSheet.Exception);
            }
        }

        private bool ValidateData(IEnumerable<dynamic> timesheetdata)
        {
            var isValid = true;

            if (timesheetdata == null || !timesheetdata.Any())
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
