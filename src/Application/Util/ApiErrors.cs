using System.Threading.Tasks;
using Flunt.Notifications;
using Fusap.TimeSheet.Application.Notifications;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;
using Newtonsoft.Json;

namespace Fusap.TimeSheet.Application.Util
{
    public static class ApiErrors<T> where T : Notifiable<Notification>
    {
        public static async Task<StatusCode> AddNotificationAsync(ApiException ex, T request)
        {
            try
            {
                var jsonstring = await ex.HttpResponseMessage?.Content.ReadAsStringAsync()!;

                if (!string.IsNullOrEmpty(jsonstring))
                {
                    var result = JsonConvert.DeserializeObject<ErrorResponse>(jsonstring);

                    if (result?.Errors != null)
                    {
                        foreach (var error in result.Errors)
                        {
                            request.AddNotification(error.ErrorCode, error.Message);
                        }
                    }
                }

                return (StatusCode)ex.HttpResponseMessage.StatusCode;
            }
            catch
            {
                request.AddNotification(ErrorCatalog.TimeSheet.ErrorDuringDataProcessing.Code, ErrorCatalog.TimeSheet.ErrorDuringDataProcessing.Message);
                return StatusCode.UnprocessableEntity;
            }
        }
    }
}
