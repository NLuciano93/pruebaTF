using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Fusap.TimeSheet.Data
{
    public partial class DbConnectionConsumerService
    {
        public async Task UpdatePasswordAsync(int registrationId, string password)
        {
            var procedure = "Usp_UpdatePasswordbyRegistrationID";

            var param = new DynamicParameters();
            param.Add("@RegistrationID", registrationId);
            param.Add("@Password", password);

            await _connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateTimeSheetStatusAsync(int timeSheetMasterId, int timeSheetStatus, string? comment)
        {
            var procedure = "Usp_UpdateTimeSheetStatus";

            var param = new DynamicParameters();
            param.Add("@TimeSheetMasterID", timeSheetMasterId);
            param.Add("@TimeSheetStatus", timeSheetStatus);
            param.Add("@Comment", comment);

            await _connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateTimeSheetMasterAsync(int timeSheetMasterId, decimal totalHours)
        {
            var procedure = "Usp_UpdateTimeSheetMaster";

            var param = new DynamicParameters();
            param.Add("@TimeSheetMasterID", timeSheetMasterId);
            param.Add("@TotalHours", totalHours);

            await _connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
        }

        public async Task DisableExistingNotificationsAsync()
        {
            var procedure = "Usp_DisableExistingNotifications";

            await _connection.ExecuteAsync(procedure, null, commandType: CommandType.StoredProcedure);
        }

        public async Task DeActivateNotificationByIdAsync(int notificationId)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("UPDATE [NotificationsTB] ");
            sqlStatement.AppendLine("SET [NotificationsTB].[Status] = 'D' ");
            sqlStatement.AppendLine($"WHERE [NotificationsTB].[NotificationsID] = {notificationId}");

            await _connection.ExecuteAsync(sqlStatement.ToString(), notificationId);
        }
    }
}
