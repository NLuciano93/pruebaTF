using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Fusap.TimeSheet.Data
{
    public partial class DbConnectionConsumerService
    {
        public async Task DeleteAsync(int projectId)
        {
            var sqlStatement = "DELETE FROM [ProjectMaster] WHERE [ProjectMaster].[ProjectID] = @ProjectId";
            await _connection.ExecuteAsync(sqlStatement, new { ProjectId = projectId });
        }

        public async Task DeleteAsync(int timeSheetMasterId, int userId)
        {
            var procedure = "Usp_DeleteTimeSheet";

            var param = new DynamicParameters();
            param.Add("@TimeSheetID", timeSheetMasterId);
            param.Add("@UserID", userId);

            await _connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
        }
    }
}
