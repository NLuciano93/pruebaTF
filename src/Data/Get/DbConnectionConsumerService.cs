using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Data
{
    public partial class DbConnectionConsumerService
    {
        public async Task<UserDetailResponse> GetUserAsync(string userName)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[Registration].[Name]");
            sqlBuilder.Select("[Registration].[Mobileno]");
            sqlBuilder.Select("[Registration].[EmailID]");
            sqlBuilder.Select("[Registration].[Username]");
            sqlBuilder.Select("[Registration].[Gender]");
            sqlBuilder.Select("[Registration].[Birthdate]");
            sqlBuilder.Select("[Roles].[Rolename]");

            var registration = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [Registration] /**join**/ /**where**/ ");

            sqlBuilder.Join("[Roles] ON [Roles].[RoleID] = [Registration].[RoleID]");

            sqlBuilder.Where("[Registration].[Username] = @userName", new { userName });

            var userDetailResponse = await _readOnlyConnection.QueryFirstOrDefaultAsync<UserDetailResponse>(registration.RawSql, registration.Parameters);

            return userDetailResponse;
        }

        public async Task<int?> ValidateUserAsync(string userName, string password)
        {
            var sqlBuilder = new SqlBuilder();

            var registration = sqlBuilder.AddTemplate("SELECT [Registration].[RegistrationID] FROM [Registration] /**where**/ ");

            sqlBuilder.Where("[Registration].[Username] = @userName", new { userName });
            sqlBuilder.Where("[Registration].[Password] = @password", new { password });

            var registrationId = await _readOnlyConnection.ExecuteScalarAsync<int?>(registration.RawSql, registration.Parameters);

            return registrationId;
        }

        public async Task<bool> CheckUserNameExistsAsync(string userName)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [Registration] /**where**/ ");

            sqlBuilder.Where("[Registration].[Username] = @userName", new { userName });

            var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

            return exists;
        }

        public async Task<IEnumerable<dynamic>> GetTimeSheetMasterIDByPeriodAsync(DateTime period)
        {
            var procedure = "Usp_GetTimeSheetByPeriod";
            var values = new { period.Month, period.Year };
            var results = await _connection.QueryAsync(procedure, values, commandType: CommandType.StoredProcedure);

            return results;
        }

        public async Task<IEnumerable<dynamic>> GetAllCodeProjectsAsync()
        {
            var sql = "SELECT ProjectCode From[ProjectMaster]";
            var projects = await _readOnlyConnection.QueryAsync(sql);

            return projects;
        }

        public async Task<IEnumerable<dynamic>> GetAllUsersAsync()
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[RegistrationID]");
            sqlBuilder.Select("[Name]");
            sqlBuilder.Select("[EmployeeID]");

            var registration = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [Registration] /**where**/ ");

            sqlBuilder.Where($"[Registration].[RoleID] = {1} OR [Registration].[RoleID] = {2}");

            var result = await _readOnlyConnection.QueryAsync(registration.RawSql, registration.Parameters);

            return result;
        }

        public async Task<IPagination<RegistrationSummaryResponse>> GetAllUsersAsync(ShowAllUsersDto showAllUsersDto)
        {
            const int PAGE_SIZE = 20;

            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                showAllUsersDto.RoleId
            });

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [Registration] /**where**/ ");

            sqlBuilder.Where("[Registration].[RoleID] = @roleId");

            if (!string.IsNullOrEmpty(showAllUsersDto.Name))
            {
                sqlBuilder.Where($"[Registration].[Name] LIKE '%{showAllUsersDto.Name}%' ");
            }

            var estimatedCount = _connection.QueryFirstOrDefault<int>(template.RawSql, template.Parameters);

            if (estimatedCount <= 0)
            {
                var result = new List<RegistrationSummaryResponse>();
                return new Pagination<RegistrationSummaryResponse>(result, 0, 0);
            }

            sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[Registration].[RegistrationID]");
            sqlBuilder.Select("[Registration].[Name]");
            sqlBuilder.Select("[Registration].[Mobileno]");
            sqlBuilder.Select("[Registration].[EmailID]");
            sqlBuilder.Select("[Registration].[Username]");

            sqlBuilder.AddParameters(parameters: new
            {
                showAllUsersDto.RoleId,
                showAllUsersDto.Offset,
                PAGE_SIZE
            });

            template = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [Registration] /**where**/ /**orderby**/ OFFSET(@offset - 1) * @PAGE_SIZE ROWS FETCH NEXT @PAGE_SIZE ROWS ONLY");

            sqlBuilder.Where("[Registration].[RoleID] = @roleId");

            if (!string.IsNullOrEmpty(showAllUsersDto.Name))
            {
                sqlBuilder.Where($"[Registration].[Name] LIKE '%{showAllUsersDto.Name}%' ");
            }

            sqlBuilder.OrderBy($"[Registration].[{showAllUsersDto.SortColumn}] {showAllUsersDto.SortDirection}");

            var documents = await _readOnlyConnection.QueryAsync<RegistrationSummaryResponse>(template.RawSql, template.Parameters);

            return documents.ToPagination(estimatedCount > PAGE_SIZE ? showAllUsersDto.Offset + PAGE_SIZE : (int?)null, estimatedCount);
        }

        public async Task<IEnumerable<dynamic>> TimesheetDetailsbyTimeSheetMasterIdAsync(int timeSheetMasterId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[TimeSheetDetails].[TimeSheetID]");
            sqlBuilder.Select("[TimeSheetDetails].[CreatedOn]");
            sqlBuilder.Select("[TimeSheetDetails].[Period]");
            sqlBuilder.Select("[TimeSheetDetails].[DaysofWeek]");
            sqlBuilder.Select("[TimeSheetDetails].[Hours]");
            sqlBuilder.Select("[ProjectMaster].[ProjectName]");
            sqlBuilder.Select("[TimeSheetDetails].[TimeSheetMasterID]");
            sqlBuilder.Select("[TimeSheetDetails].[UserID]");
            sqlBuilder.Select("[ProjectMaster].[ProjectID]");

            var details = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [TimeSheetDetails] /**join**/ /**where**/ ");

            sqlBuilder.Join("[ProjectMaster] ON [ProjectMaster].[ProjectID] = [TimeSheetDetails].[ProjectID]");
            sqlBuilder.Join("[Registration] ON [TimeSheetDetails].[UserID] = [Registration].[RegistrationID]");
            sqlBuilder.Where("[TimeSheetDetails].[TimeSheetMasterID] = @timeSheetMasterID", new { timeSheetMasterId });

            var result = await _readOnlyConnection.QueryAsync(details.RawSql, details.Parameters);

            return result;
        }

        public async Task<string> GetProjectCodeByIdAsync(int projectId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[ProjectMaster].[ProjectCode]");

            var projectCodeQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster] /**where**/ ");
            sqlBuilder.Where("[ProjectMaster].[ProjectID] = @projectID", new { projectId });

            var projectCode = await _readOnlyConnection.QueryFirstOrDefaultAsync(projectCodeQuery.RawSql, projectCodeQuery.Parameters);

            return projectCode.ProjectCode;
        }

        public async Task<string> GetProjectNameByIdAsync(int projectId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[ProjectMaster].[ProjectName]");

            var projectNameQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster] /**where**/ ");
            sqlBuilder.Where("[ProjectMaster].[ProjectID] = @projectID", new { projectId });

            var projectName = await _readOnlyConnection.QueryFirstOrDefaultAsync<string>(projectNameQuery.RawSql, projectNameQuery.Parameters);

            return projectName;
        }

        public async Task<string> GetUserByIdAsync(int userId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[Registration].[Name]");

            var userQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [Registration] /**where**/ ");
            sqlBuilder.Where("[Registration].[RegistrationID] = @userID", new { userId });

            var userName = await _readOnlyConnection.QueryFirstOrDefaultAsync(userQuery.RawSql, userQuery.Parameters);

            return userName.Name;
        }

        public async Task<List<ProjectMasterResponse>> ListOfProjectsAsync()
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[ProjectMaster].[ProjectID]");
            sqlBuilder.Select("[ProjectMaster].[ProjectName]");
            sqlBuilder.Select("[ProjectMaster].[NatureofIndustry]");
            sqlBuilder.Select("[ProjectMaster].[ProjectCode]");

            var userQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster]");

            var projects = await _readOnlyConnection.QueryAsync<ProjectMasterResponse>(userQuery.RawSql, userQuery.Parameters);

            return projects.AsList();
        }

        public async Task<List<ProjectMasterResponse>> ListofProjectsByUserAdminAsync(int userId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                userId
            });

            sqlBuilder.Select("[ProjectMaster].[ProjectID]");
            sqlBuilder.Select("[ProjectMaster].[ProjectName]");
            sqlBuilder.Select("[ProjectMaster].[NatureofIndustry]");
            sqlBuilder.Select("[ProjectMaster].[ProjectCode]");

            var userQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster] /**join**/ /**where**/ ");

            sqlBuilder.Join("[AssignedProjectToUsersAdmins] ON [AssignedProjectToUsersAdmins].[ProjectID] = [ProjectMaster].[ProjectID]");

            sqlBuilder.Where("[AssignedProjectToUsersAdmins].[RegistrationID] = @userId");

            var projects = await _readOnlyConnection.QueryAsync<ProjectMasterResponse>(userQuery.RawSql, userQuery.Parameters);

            return projects.AsList();
        }

        public async Task<List<ProjectMasterResponse>> ListofProjectsByUserAsync(int userId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                userId
            });

            sqlBuilder.Select("[ProjectMaster].[ProjectID]");
            sqlBuilder.Select("[ProjectMaster].[ProjectName]");
            sqlBuilder.Select("[ProjectMaster].[NatureofIndustry]");
            sqlBuilder.Select("[ProjectMaster].[ProjectCode]");

            var userQuery = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster] /**join**/ /**where**/ ");

            sqlBuilder.Join("[AssignedProjectToUsers] ON [AssignedProjectToUsers].[ProjectID] = [ProjectMaster].[ProjectID]");

            sqlBuilder.Where("[AssignedProjectToUsers].[RegistrationID] = @userId");

            var projects = await _readOnlyConnection.QueryAsync<ProjectMasterResponse>(userQuery.RawSql, userQuery.Parameters);

            return projects.AsList();
        }

        public async Task<int> GetTotalProjectsCountsAsync()
        {
            var procedure = "Usp_GetProjectCount";

            var results = await _readOnlyConnection.ExecuteScalarAsync<int>(procedure, null, commandType: CommandType.StoredProcedure);
            return results;
        }

        public async Task<List<AssignedRolesResponse.AdminModel>> ListOfAdminsAsync()
        {
            var procedure = "Usp_GetListofAdmins";
            var results = await _readOnlyConnection.QueryAsync<AssignedRolesResponse.AdminModel>(procedure, null, commandType: CommandType.StoredProcedure);

            return results.AsList();
        }

        public async Task<List<AssignedRolesResponse.UserModel>> ListOfUserAsync()
        {
            var procedure = "Usp_GetListofUnAssignedUsers";
            var results = await _readOnlyConnection.QueryAsync<AssignedRolesResponse.UserModel>(procedure, null, commandType: CommandType.StoredProcedure);

            return results.AsList();
        }

        public async Task<bool> ValidateAssignationAsync(AssignProjectToUsersDto assignProjectToUsersDto)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [AssignedProjectToUsers] /**where**/ ");

            sqlBuilder.Where("[AssignedProjectToUsers].[ProjectID] = @ProjectID", new { assignProjectToUsersDto.ProjectId });

            for (var i = 0; i < assignProjectToUsersDto.ListOfUsers.Count; i++)
            {
                if (assignProjectToUsersDto.ListOfUsers[i].SelectedUsers)
                {
                    sqlBuilder.Where("[AssignedProjectToUsers].[RegistrationID] = @RegistrationID", new { assignProjectToUsersDto.ListOfUsers[i].RegistrationId });

                    var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

                    if (exists)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> CheckProjectIdExistsInTimesheetAsync(int projectId)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [TimeSheetDetails] /**where**/ ");

            sqlBuilder.Where("[TimeSheetDetails].[ProjectID] = @projectId", new { projectId });

            var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

            return exists;
        }

        public async Task<bool> CheckProjectIdExistsInExpenseAsync(int projectId)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [Expense] /**where**/ ");

            sqlBuilder.Where("[Expense].[ProjectID] = @projectId", new { projectId });

            var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

            return exists;
        }

        public async Task<bool> CheckProjectCodeExistsAsync(string projectCode)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [ProjectMaster] /**where**/ ");

            sqlBuilder.Where("[ProjectMaster].[ProjectCode] = @projectCode", new { projectCode });

            var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

            return exists;
        }

        public async Task<bool> CheckProjectNameExistsAsync(string projectName)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [ProjectMaster] /**where**/ ");

            sqlBuilder.Where("[ProjectMaster].[ProjectName] = @projectName", new { projectName });

            var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

            return exists;
        }

        public async Task<bool> CheckProjectIdExistsInTimeSheetAsync(int projectId)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT COUNT(DISTINCT [TimeSheetDetails].[ProjectID]) FROM [TimeSheetDetails] /**where**/ ");

            sqlBuilder.Where("[TimeSheetDetails].[ProjectID] = @projectId", new { projectId });

            var exists = await _readOnlyConnection.ExecuteScalarAsync<bool>(template.RawSql, template.Parameters);

            return exists;
        }

        public async Task<IPagination<UsersHoursNoCompleteResponse>> GetUsersHoursNoCompleteAsync(UsersHoursNoCompleteDto usersHoursNoCompleteDto)
        {
            const int PAGE_SIZE = 20;

            var sqlBuilderInClause = new SqlBuilder();

            var inClauseTemplate = sqlBuilderInClause.AddTemplate("SELECT DISTINCT [RegistrationID] FROM [Registration] /**join**/ /**where**/ ");

            sqlBuilderInClause.Join("[TimeSheetMaster] ON [TimeSheetMaster].[UserID] = [Registration].[RegistrationID]");

            sqlBuilderInClause.Where("[TimeSheetMaster].[FromDate] <= @date AND [TimeSheetMaster].[ToDate] >= @date");

            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new { usersHoursNoCompleteDto.Date });

            var template = sqlBuilder.AddTemplate($"SELECT COUNT(1) FROM [Registration] /**where**/ ");

            sqlBuilder.Where($"[Registration].[RoleID] = {1} OR [Registration].[RoleID] = {2}");

            sqlBuilder.Where($"[Registration].[RegistrationID] NOT IN({inClauseTemplate.RawSql})");

            var estimatedCount = _connection.QueryFirstOrDefault<int>(template.RawSql, template.Parameters);

            if (estimatedCount <= 0)
            {
                var result = new List<UsersHoursNoCompleteResponse>();
                return new Pagination<UsersHoursNoCompleteResponse>(result, 0, 0);
            }

            sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[RegistrationID] AS [UserId]");
            sqlBuilder.Select("[Registration].[Username]");
            sqlBuilder.Select("[Registration].[Name]");
            sqlBuilder.Select("[Registration].[Mobileno]");
            sqlBuilder.Select("[Registration].[EmailID] AS [Email]");

            sqlBuilder.AddParameters(parameters: new
            {
                usersHoursNoCompleteDto.Date,
                usersHoursNoCompleteDto.Offset,
                PAGE_SIZE
            });

            template = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [Registration] /**where**/ /**orderby**/ OFFSET(@offset - 1) * @PAGE_SIZE ROWS FETCH NEXT @PAGE_SIZE ROWS ONLY");

            sqlBuilder.Where($"[Registration].[RoleID] = {1} OR [Registration].[RoleID] = {2}");

            sqlBuilder.Where($"[Registration].[RegistrationID] NOT IN({inClauseTemplate.RawSql})");

            sqlBuilder.OrderBy($"[Registration].[{usersHoursNoCompleteDto.SortColumn}] {usersHoursNoCompleteDto.SortDirection}");

            var documents = await _readOnlyConnection.QueryAsync<UsersHoursNoCompleteResponse>(template.RawSql, template.Parameters);

            return documents.ToPagination(estimatedCount > PAGE_SIZE ? usersHoursNoCompleteDto.Offset + PAGE_SIZE : (int?)null, estimatedCount);
        }

        public async Task<RegistrationViewDetailsResponse> GetUserDetailsByRegistrationIdAsync(int registrationId)
        {
            var procedure = "Usp_GetUserDetailsByRegistrationID";
            var values = new { registrationId };
            var results = await _readOnlyConnection.QueryAsync<RegistrationViewDetailsResponse>(procedure, values, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

        public async Task<RegistrationViewDetailsResponse> GetAdminDetailsByRegistrationIdAsync(int registrationId)
        {
            var procedure = "Usp_GetAdminDetailsByRegistrationID";
            var values = new { registrationId };
            var results = await _readOnlyConnection.QueryAsync<RegistrationViewDetailsResponse>(procedure, values, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

        public async Task<int> GetTotalAdminsCountAsync()
        {
            var procedure = "Usp_GetAdminCount";
            var results = await _readOnlyConnection.ExecuteScalarAsync<int>(procedure, null, commandType: CommandType.StoredProcedure);
            return results;
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            var procedure = "Usp_GetUsersCount";
            var results = await _readOnlyConnection.ExecuteScalarAsync<int>(procedure, null, commandType: CommandType.StoredProcedure);
            return results;
        }

        public async Task<IPagination<RegistrationSummaryResponse>> GetAllUsersUnderAdminAsync(ShowAllUsersUnderAdminDto showAllUsersUnderAdminDto)
        {
            const int PAGE_SIZE = 20;

            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                showAllUsersUnderAdminDto.AdminUserId,
                showAllUsersUnderAdminDto.Name
            });

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [Registration] /**join**/ /**where**/ ");

            sqlBuilder.Join("[AssignedRoles] ON [AssignedRoles].[RegistrationID] = [Registration].[RegistrationID]");

            sqlBuilder.Where("[Registration].[RoleID] = 2");
            sqlBuilder.Where("[AssignedRoles].[AssignToAdmin] = @AdminUserId");

            if (!string.IsNullOrEmpty(showAllUsersUnderAdminDto.Name))
            {
                sqlBuilder.Where("[Registration].[Name] = @Name");
            }

            var estimatedCount = _connection.QueryFirstOrDefault<int>(template.RawSql, template.Parameters);

            if (estimatedCount <= 0)
            {
                var result = new List<RegistrationSummaryResponse>();
                return new Pagination<RegistrationSummaryResponse>(result, 0, 0);
            }

            sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[Registration].[RegistrationID]");
            sqlBuilder.Select("[Registration].[Name]");
            sqlBuilder.Select("[Registration].[Mobileno]");
            sqlBuilder.Select("[Registration].[EmailID]");
            sqlBuilder.Select("[Registration].[Username]");

            sqlBuilder.AddParameters(parameters: new
            {
                showAllUsersUnderAdminDto.AdminUserId,
                showAllUsersUnderAdminDto.Name,
                showAllUsersUnderAdminDto.Offset,
                PAGE_SIZE
            });

            template = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [Registration] /**join**/ /**where**/ /**orderby**/ OFFSET(@offset - 1) * @PAGE_SIZE ROWS FETCH NEXT @PAGE_SIZE ROWS ONLY");

            sqlBuilder.Join("[AssignedRoles] ON [AssignedRoles].[RegistrationID] = [Registration].[RegistrationID]");

            sqlBuilder.Where("[Registration].[RoleID] = 2");
            sqlBuilder.Where("[AssignedRoles].[AssignToAdmin] = @AdminUserId");

            if (!string.IsNullOrEmpty(showAllUsersUnderAdminDto.Name))
            {
                sqlBuilder.Where("[Registration].[Name] = @Name");
            }

            sqlBuilder.OrderBy($"[Registration].[{showAllUsersUnderAdminDto.SortColumn}] {showAllUsersUnderAdminDto.SortDirection}");

            var documents = await _readOnlyConnection.QueryAsync<RegistrationSummaryResponse>(template.RawSql, template.Parameters);

            return documents.ToPagination(estimatedCount > PAGE_SIZE ? showAllUsersUnderAdminDto.Offset + PAGE_SIZE : (int?)null, estimatedCount);
        }

        public async Task<ProjectMasterResponse> GetProjectMasterByIdAsync(int projectId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[ProjectMaster].[ProjectID]");
            sqlBuilder.Select("[ProjectMaster].[ProjectName]");
            sqlBuilder.Select("[ProjectMaster].[NatureofIndustry]");
            sqlBuilder.Select("[ProjectMaster].[ProjectCode]");

            var details = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster] /**where**/ ");

            sqlBuilder.Where($"[ProjectMaster].[ProjectID] = @projectId", new { projectId });

            var result = await _readOnlyConnection.QueryFirstOrDefaultAsync<ProjectMasterResponse>(details.RawSql, details.Parameters);

            return result;
        }

        public async Task<IPagination<ProjectMasterResponse>> GetAllProjectsAsync(ShowAllProjectsDto showAllProjectsDto)
        {
            const int PAGE_SIZE = 20;

            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                showAllProjectsDto.ProjectCode,
                showAllProjectsDto.ProjectName
            });

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [ProjectMaster] /**where**/ ");

            if (!string.IsNullOrEmpty(showAllProjectsDto.ProjectName))
            {
                sqlBuilder.OrWhere("[ProjectMaster].[ProjectName] = @projectName");
            }

            if (!string.IsNullOrEmpty(showAllProjectsDto.ProjectCode))
            {
                sqlBuilder.OrWhere("[ProjectMaster].[ProjectCode] = @projectCode");
            }

            var estimatedCount = _connection.QueryFirstOrDefault<int>(template.RawSql, template.Parameters);

            if (estimatedCount <= 0)
            {
                var result = new List<ProjectMasterResponse>();
                return new Pagination<ProjectMasterResponse>(result, 0, 0);
            }

            sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[ProjectMaster].[ProjectID]");
            sqlBuilder.Select("[ProjectMaster].[ProjectName]");
            sqlBuilder.Select("[ProjectMaster].[NatureofIndustry]");
            sqlBuilder.Select("[ProjectMaster].[ProjectCode]");

            sqlBuilder.AddParameters(parameters: new
            {
                showAllProjectsDto.ProjectCode,
                showAllProjectsDto.ProjectName,
                showAllProjectsDto.Offset,
                PAGE_SIZE
            });

            template = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [ProjectMaster] /**where**/ /**orderby**/ OFFSET(@offset - 1) * @PAGE_SIZE ROWS FETCH NEXT @PAGE_SIZE ROWS ONLY");

            if (!string.IsNullOrEmpty(showAllProjectsDto.ProjectName))
            {
                sqlBuilder.OrWhere("[ProjectMaster].[ProjectName] = @projectName");
            }

            if (!string.IsNullOrEmpty(showAllProjectsDto.ProjectCode))
            {
                sqlBuilder.OrWhere("[ProjectMaster].[ProjectCode] = @projectCode");
            }

            sqlBuilder.OrderBy($"[ProjectMaster].[{showAllProjectsDto.SortColumn}] {showAllProjectsDto.SortDirection}");

            var documents = await _readOnlyConnection.QueryAsync<ProjectMasterResponse>(template.RawSql, template.Parameters);

            return documents.ToPagination(estimatedCount > PAGE_SIZE ? showAllProjectsDto.Offset + PAGE_SIZE : (int?)null, estimatedCount);
        }

        public async Task<IPagination<NotificationTbResponse>> GetNotificationsTbAsync(ShowNotificationsDto showNotificationsDto)
        {
            const int PAGE_SIZE = 20;

            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                showNotificationsDto.Message
            });

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [NotificationsTB] /**where**/ ");

            if (!string.IsNullOrEmpty(showNotificationsDto.Message))
            {
                sqlBuilder.Where($"[NotificationsTB].[Message] LIKE '%{showNotificationsDto.Message}%' ");
            }

            var estimatedCount = _connection.QueryFirstOrDefault<int>(template.RawSql, template.Parameters);

            if (estimatedCount <= 0)
            {
                var result = new List<NotificationTbResponse>();
                return new Pagination<NotificationTbResponse>(result, 0, 0);
            }

            sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[NotificationsTB].[NotificationsID]");
            sqlBuilder.Select("[NotificationsTB].[Status]");
            sqlBuilder.Select("[NotificationsTB].[Message]");
            sqlBuilder.Select("[NotificationsTB].[CreatedOn]");
            sqlBuilder.Select("[NotificationsTB].[FromDate]");
            sqlBuilder.Select("[NotificationsTB].[ToDate]");

            sqlBuilder.AddParameters(parameters: new
            {
                showNotificationsDto.Message,
                showNotificationsDto.Offset,
                PAGE_SIZE
            });

            template = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [NotificationsTB] /**where**/ /**orderby**/ OFFSET(@offset - 1) * @PAGE_SIZE ROWS FETCH NEXT @PAGE_SIZE ROWS ONLY");


            if (!string.IsNullOrEmpty(showNotificationsDto.Message))
            {
                sqlBuilder.Where($"[NotificationsTB].[Message] LIKE '%{showNotificationsDto.Message}%' ");
            }

            sqlBuilder.OrderBy($"[NotificationsTB].[{showNotificationsDto.SortColumn}] {showNotificationsDto.SortDirection}");

            var documents = await _readOnlyConnection.QueryAsync<NotificationTbResponse>(template.RawSql, template.Parameters);

            return documents.ToPagination(estimatedCount > PAGE_SIZE ? showNotificationsDto.Offset + PAGE_SIZE : (int?)null, estimatedCount);
        }

        public async Task<TimeSheetMasterResponse> GetTimeSheetMasterByIdAsync(int timeSheetMasterId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[TimeSheetMaster].[TimeSheetMasterID]");
            sqlBuilder.Select("[TimeSheetMaster].[FromDate]");
            sqlBuilder.Select("[TimeSheetMaster].[ToDate]");
            sqlBuilder.Select("[TimeSheetMaster].[TotalHours]");
            sqlBuilder.Select("[TimeSheetMaster].[UserID]");
            sqlBuilder.Select("[TimeSheetMaster].[CreatedOn]");
            sqlBuilder.Select("[TimeSheetMaster].[Comment]");
            sqlBuilder.Select("[TimeSheetMaster].[TimeSheetStatus]");

            var details = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [TimeSheetMaster] /**where**/ ");

            sqlBuilder.Where($"[TimeSheetMaster].[TimeSheetMasterID] = @timeSheetMasterId", new { timeSheetMasterId });

            var result = await _readOnlyConnection.QueryFirstOrDefaultAsync<TimeSheetMasterResponse>(details.RawSql, details.Parameters);

            return result;
        }

        public async Task<TimeSheetDisplayViewResponse> GetTimeSheetsCountByAdminIdAsync(int adminId)
        {
            var procedure = "Usp_GetTimeSheetsCountByAdminID";

            var param = new DynamicParameters();
            param.Add("@AdminID", adminId);

            var results = await _readOnlyConnection.QueryAsync<TimeSheetDisplayViewResponse>(procedure, param, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

        public async Task<TimeSheetDisplayViewResponse> GetTimeSheetsCountByUserIdAsync(int userId)
        {
            var procedure = "Usp_GetTimeSheetsCountByUserID";

            var param = new DynamicParameters();
            param.Add("@UserID", userId);

            var results = await _readOnlyConnection.QueryAsync<TimeSheetDisplayViewResponse>(procedure, param, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

        public async Task<IPagination<TimeSheetMasterResponse>> GetAllTimeSheetsAsync(ShowAllTimeSheetsDto showAllTimeSheetsDto)
        {
            const int PAGE_SIZE = 20;

            var sqlBuilder = new SqlBuilder();

            sqlBuilder.AddParameters(parameters: new
            {
                showAllTimeSheetsDto.UserId,
                showAllTimeSheetsDto.TimeSheetStatus
            });

            var template = sqlBuilder.AddTemplate("SELECT COUNT(1) FROM [TimeSheetMaster] /**where**/ ");

            if (showAllTimeSheetsDto.UserId > 0)
            {
                sqlBuilder.OrWhere("[TimeSheetMaster].[UserID] = @UserId");
            }

            if (showAllTimeSheetsDto.TimeSheetStatus > 0 && showAllTimeSheetsDto.TimeSheetStatus < 4)
            {
                sqlBuilder.OrWhere("[TimeSheetMaster].[TimeSheetStatus] = @TimeSheetStatus");
            }

            var estimatedCount = _connection.QueryFirstOrDefault<int>(template.RawSql, template.Parameters);

            if (estimatedCount <= 0)
            {
                var result = new List<TimeSheetMasterResponse>();
                return new Pagination<TimeSheetMasterResponse>(result, 0, 0);
            }

            sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[TimeSheetMaster].[TimeSheetMasterID]");
            sqlBuilder.Select("[TimeSheetMaster].[FromDate]");
            sqlBuilder.Select("[TimeSheetMaster].[ToDate]");
            sqlBuilder.Select("[TimeSheetMaster].[TotalHours]");
            sqlBuilder.Select("[TimeSheetMaster].[UserID]");
            sqlBuilder.Select("[TimeSheetMaster].[CreatedOn]");
            sqlBuilder.Select("[TimeSheetMaster].[Comment]");
            sqlBuilder.Select("[TimeSheetMaster].[TimeSheetStatus]");

            sqlBuilder.AddParameters(parameters: new
            {
                showAllTimeSheetsDto.UserId,
                showAllTimeSheetsDto.TimeSheetStatus,
                showAllTimeSheetsDto.Offset,
                PAGE_SIZE
            });

            template = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [TimeSheetMaster] /**where**/ /**orderby**/ OFFSET(@offset - 1) * @PAGE_SIZE ROWS FETCH NEXT @PAGE_SIZE ROWS ONLY");

            if (showAllTimeSheetsDto.UserId > 0)
            {
                sqlBuilder.OrWhere("[TimeSheetMaster].[UserID] = @UserId");
            }

            if (showAllTimeSheetsDto.TimeSheetStatus > 0 && showAllTimeSheetsDto.TimeSheetStatus < 4)
            {
                sqlBuilder.OrWhere("[TimeSheetMaster].[TimeSheetStatus] = @TimeSheetStatus");
            }

            sqlBuilder.OrderBy($"[TimeSheetMaster].[{showAllTimeSheetsDto.SortColumn}] {showAllTimeSheetsDto.SortDirection}");

            var documents = await _readOnlyConnection.QueryAsync<TimeSheetMasterResponse>(template.RawSql, template.Parameters);

            return documents.ToPagination(estimatedCount > PAGE_SIZE ? showAllTimeSheetsDto.Offset + PAGE_SIZE : (int?)null, estimatedCount);
        }

        public async Task<DescriptionTbResponse> GetDescriptionTbAsync(int projectId, int timeSheetMasterId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[DescriptionTB].[DescriptionID]");
            sqlBuilder.Select("[DescriptionTB].[Description]");
            sqlBuilder.Select("[DescriptionTB].[ProjectID]");
            sqlBuilder.Select("[DescriptionTB].[TimeSheetMasterID]");
            sqlBuilder.Select("[DescriptionTB].[CreatedOn]");
            sqlBuilder.Select("[DescriptionTB].[UserID]");

            var details = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [DescriptionTB] /**where**/ ");

            sqlBuilder.Where("[DescriptionTB].[ProjectID] = @projectId AND [DescriptionTB].[TimeSheetMasterID] = @timeSheetMasterId", new { projectId, timeSheetMasterId });

            var result = await _readOnlyConnection.QueryFirstOrDefaultAsync<DescriptionTbResponse>(details.RawSql, details.Parameters);

            return result;
        }

        public async Task<UserDateIsUsedResponse> GetUsersDateIsUsedAsync(DateTime fromDate, int userId)
        {
            var sqlBuilder = new SqlBuilder();

            sqlBuilder.Select("[TimeSheetDetails].[TimeSheetID]");
            sqlBuilder.Select("[TimeSheetDetails].[DaysofWeek]");
            sqlBuilder.Select("[TimeSheetDetails].[Hours]");
            sqlBuilder.Select("[TimeSheetDetails].[Period]");
            sqlBuilder.Select("[TimeSheetDetails].[ProjectID]");
            sqlBuilder.Select("[TimeSheetDetails].[UserID]");
            sqlBuilder.Select("[TimeSheetDetails].[CreatedOn]");
            sqlBuilder.Select("[TimeSheetDetails].[TimeSheetMasterID]");
            sqlBuilder.Select("[TimeSheetDetails].[TotalHours]");

            var details = sqlBuilder.AddTemplate("SELECT /**select**/ FROM [TimeSheetDetails] /**where**/ ");

            sqlBuilder.Where("[TimeSheetDetails].[Period] = @fromDate AND [TimeSheetDetails].[UserID] = @userId", new { fromDate, userId });

            var result = await _readOnlyConnection.QueryFirstOrDefaultAsync<UserDateIsUsedResponse>(details.RawSql, details.Parameters);

            return result;
        }

        public async Task<string> GetStoredPasswordAsync(int registrationId)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT [Registration].[Password] FROM [Registration] /**where**/ ");
            sqlBuilder.Where("[Registration].[RegistrationID] = @registrationID", new { registrationId });

            var storedPassword = await _readOnlyConnection.QueryFirstOrDefaultAsync<string>(template.RawSql, template.Parameters);

            return storedPassword;
        }

        public async Task<ExpenseDisplayViewResponse> GetExpesesCountByAdminIdAsync(int adminId)
        {
            var procedure = "Usp_GetExpenseAuditCountByAdminID";

            var param = new DynamicParameters();
            param.Add("@AdminID", adminId);

            var results = await _readOnlyConnection.QueryAsync<ExpenseDisplayViewResponse>(procedure, param, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

        public async Task<int> GetRegistrationIdByRegistrationUsernameAsync(string userName)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT [Registration].[RegistrationID] FROM [Registration] /**where**/ ");
            sqlBuilder.Where("[Registration].[Username] = @userName", new { userName });

            var registrationId = await _readOnlyConnection.QueryFirstOrDefaultAsync<int>(template.RawSql, template.Parameters);

            return registrationId;
        }

        public async Task<int> GetRoleIdbyRolenameAsync(string roleName)
        {
            var sqlBuilder = new SqlBuilder();

            var template = sqlBuilder.AddTemplate("SELECT [Roles].[RoleID] FROM [Roles] /**where**/ ");
            sqlBuilder.Where("[Roles].[Rolename] = @roleName", new { roleName });

            var roleId = await _readOnlyConnection.QueryFirstOrDefaultAsync<int>(template.RawSql, template.Parameters);

            return roleId;
        }
    }
}
