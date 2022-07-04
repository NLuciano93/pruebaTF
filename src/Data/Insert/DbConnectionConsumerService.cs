using System.Text;
using System.Threading.Tasks;
using Dapper;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;

namespace Fusap.TimeSheet.Data
{
    public partial class DbConnectionConsumerService
    {
        public async Task<int> InsertAsync(Registration registration)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [Registration]");
            sqlStatement.AppendLine("([Name]");
            sqlStatement.AppendLine(",[Mobileno]");
            sqlStatement.AppendLine(",[EmailID]");
            sqlStatement.AppendLine(",[Username]");
            sqlStatement.AppendLine(",[Password]");
            sqlStatement.AppendLine(",[ConfirmPassword]");
            sqlStatement.AppendLine(",[Gender]");
            sqlStatement.AppendLine(",[Birthdate]");
            sqlStatement.AppendLine(",[RoleID]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[EmployeeID]");
            sqlStatement.AppendLine(",[DateofJoining]");
            sqlStatement.AppendLine(",[ForceChangePassword])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@Name");
            sqlStatement.AppendLine(",@Mobileno");
            sqlStatement.AppendLine(",@EmailID");
            sqlStatement.AppendLine(",@Username");
            sqlStatement.AppendLine(",@Password");
            sqlStatement.AppendLine(",@ConfirmPassword");
            sqlStatement.AppendLine(",@Gender");
            sqlStatement.AppendLine(",@Birthdate");
            sqlStatement.AppendLine(",@RoleID");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@EmployeeID");
            sqlStatement.AppendLine(",@DateofJoining");
            sqlStatement.AppendLine(",@ForceChangePassword)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), registration);
        }

        public async Task<int> InsertAsync(ProjectMaster projectMaster)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [ProjectMaster]");
            sqlStatement.AppendLine("([ProjectName]");
            sqlStatement.AppendLine(",[NatureofIndustry]");
            sqlStatement.AppendLine(",[ProjectCode])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@ProjectName");
            sqlStatement.AppendLine(",@NatureofIndustry");
            sqlStatement.AppendLine(",@ProjectCode)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), projectMaster);
        }

        public async Task<int> InsertAsync(Expense expense)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [Expense]");
            sqlStatement.AppendLine("([PurposeorReason]");
            sqlStatement.AppendLine(",[ExpenseStatus]");
            sqlStatement.AppendLine(",[FromDate]");
            sqlStatement.AppendLine(",[ToDate]");
            sqlStatement.AppendLine(",[VoucherID]");
            sqlStatement.AppendLine(",[HotelBills]");
            sqlStatement.AppendLine(",[TravelBills]");
            sqlStatement.AppendLine(",[MealsBills]");
            sqlStatement.AppendLine(",[LandLineBills]");
            sqlStatement.AppendLine(",[TransportBills]");
            sqlStatement.AppendLine(",[MobileBills]");
            sqlStatement.AppendLine(",[Miscellaneous]");
            sqlStatement.AppendLine(",[TotalAmount]");
            sqlStatement.AppendLine(",[UserID]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[Comment]");
            sqlStatement.AppendLine(",[ProjectID])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@PurposeorReason");
            sqlStatement.AppendLine(",@ExpenseStatus");
            sqlStatement.AppendLine(",@FromDate");
            sqlStatement.AppendLine(",@ToDate");
            sqlStatement.AppendLine(",@VoucherId");
            sqlStatement.AppendLine(",@HotelBills");
            sqlStatement.AppendLine(",@TravelBills");
            sqlStatement.AppendLine(",@MealsBills");
            sqlStatement.AppendLine(",@LandLineBills");
            sqlStatement.AppendLine(",@TransportBills");
            sqlStatement.AppendLine(",@MobileBills");
            sqlStatement.AppendLine(",@Miscellaneous");
            sqlStatement.AppendLine(",@TotalAmount");
            sqlStatement.AppendLine(",@UserID");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@Comment");
            sqlStatement.AppendLine(",@ProjectId)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), expense);
        }

        public async Task<int> InsertAsync(TimeSheetMaster timeSheetMaster)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [TimeSheetMaster]");
            sqlStatement.AppendLine("([FromDate]");
            sqlStatement.AppendLine(",[ToDate]");
            sqlStatement.AppendLine(",[TotalHours]");
            sqlStatement.AppendLine(",[UserID]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[Comment]");
            sqlStatement.AppendLine(",[TimeSheetStatus])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@FromDate");
            sqlStatement.AppendLine(",@ToDate");
            sqlStatement.AppendLine(",@TotalHours");
            sqlStatement.AppendLine(",@UserId");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@Comment");
            sqlStatement.AppendLine(",@TimeSheetStatus)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), timeSheetMaster);
        }

        public async Task<int> InsertAsync(DescriptionTb descriptionTb)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [DescriptionTB]");
            sqlStatement.AppendLine("([Description]");
            sqlStatement.AppendLine(",[ProjectID]");
            sqlStatement.AppendLine(",[TimeSheetMasterID]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[UserID])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@Description");
            sqlStatement.AppendLine(",@ProjectId");
            sqlStatement.AppendLine(",@TimeSheetMasterId");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@UserId)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), descriptionTb);
        }

        public async Task<int> InsertAsync(TimeSheetDetails timeSheetDetails)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [TimeSheetDetails]");
            sqlStatement.AppendLine("([DaysofWeek]");
            sqlStatement.AppendLine(",[Hours]");
            sqlStatement.AppendLine(",[Period]");
            sqlStatement.AppendLine(",[ProjectID]");
            sqlStatement.AppendLine(",[UserID]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[TimeSheetMasterID]");
            sqlStatement.AppendLine(",[TotalHours])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@DaysofWeek");
            sqlStatement.AppendLine(",@Hours");
            sqlStatement.AppendLine(",@Period");
            sqlStatement.AppendLine(",@ProjectId");
            sqlStatement.AppendLine(",@UserId");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@TimeSheetMasterId");
            sqlStatement.AppendLine(",@TotalHours)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), timeSheetDetails);
        }

        public async Task<int> InsertAsync(TimeSheetAuditTb timeSheetAuditTb)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [TimeSheetAuditTB]");
            sqlStatement.AppendLine("([ApprovalUser]");
            sqlStatement.AppendLine(",[ProcessedDate]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[Comment]");
            sqlStatement.AppendLine(",[Status]");
            sqlStatement.AppendLine(",[TimeSheetID]");
            sqlStatement.AppendLine(",[UserID])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@ApprovalUser");
            sqlStatement.AppendLine(",@ProcessedDate");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@Comment");
            sqlStatement.AppendLine(",@Status");
            sqlStatement.AppendLine(",@TimeSheetId");
            sqlStatement.AppendLine(",@UserId)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), timeSheetAuditTb);
        }

        public async Task<int> InsertAsync(NotificationsTb notificationsTb)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO[dbo].[NotificationsTB]");
            sqlStatement.AppendLine("([Status]");
            sqlStatement.AppendLine(",[Message]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[FromDate]");
            sqlStatement.AppendLine(",[ToDate])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@Status");
            sqlStatement.AppendLine(",@Message");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@FromDate");
            sqlStatement.AppendLine(",@ToDate)");

            sqlStatement.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            return await _connection.ExecuteScalarAsync<int>(sqlStatement.ToString(), notificationsTb);
        }

        public void Insert(AssignedRoles assignedRoles)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [AssignedRoles]");
            sqlStatement.AppendLine("([AssignToAdmin]");
            sqlStatement.AppendLine(",[Status]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[CreatedBy]");
            sqlStatement.AppendLine(",[RegistrationID])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@AssignToAdmin");
            sqlStatement.AppendLine(",@Status");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@CreatedBy");
            sqlStatement.AppendLine(",@RegistrationId)");

            _connection.Execute(sqlStatement.ToString(), assignedRoles);
        }

        public void Insert(AssignedProjectToUsers assignedProjectToUsers)
        {
            var sqlStatement = new StringBuilder();

            sqlStatement.AppendLine("INSERT INTO [AssignedProjectToUsers]");
            sqlStatement.AppendLine("([ProjectID]");
            sqlStatement.AppendLine(",[RegistrationID]");
            sqlStatement.AppendLine(",[CreatedOn]");
            sqlStatement.AppendLine(",[CreatedBy])");
            sqlStatement.AppendLine("VALUES");
            sqlStatement.AppendLine("(@ProjectId");
            sqlStatement.AppendLine(",@RegistrationId");
            sqlStatement.AppendLine(",@CreatedOn");
            sqlStatement.AppendLine(",@CreatedBy)");

            _connection.Execute(sqlStatement.ToString(), assignedProjectToUsers);
        }
    }
}
