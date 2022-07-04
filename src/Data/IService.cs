using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusap.Common.Model;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.DataTranferObject;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Entities;
using Fusap.TimeSheet.Domain.TimeSheetAggregate.Response;

namespace Fusap.TimeSheet.Data
{
    public interface IService
    {
        public Task<UserDetailResponse> GetUserAsync(string userName);
        public Task<int?> ValidateUserAsync(string userName, string password);
        public Task<bool> CheckUserNameExistsAsync(string userName);
        public Task<int> InsertAsync(Registration registration);
        public Task<int> InsertAsync(ProjectMaster projectMaster);
        public Task<int> InsertAsync(Expense expense);
        public Task<int> InsertAsync(TimeSheetMaster timeSheetMaster);
        public Task<int> InsertAsync(DescriptionTb descriptionTb);
        public Task<int> InsertAsync(TimeSheetDetails timeSheetDetails);
        public Task<int> InsertAsync(TimeSheetAuditTb timeSheetAuditTb);
        public Task<int> InsertAsync(NotificationsTb notificationsTb);
        public Task DeleteAsync(int projectId);
        public Task DeleteAsync(int timeSheetMasterId, int userId);
        public void Insert(AssignedRoles assignedRoles);
        public void Insert(AssignedProjectToUsers assignedProjectToUsers);
        public Task<IEnumerable<dynamic>> GetTimeSheetMasterIDByPeriodAsync(DateTime period);
        public Task<IEnumerable<dynamic>> GetAllCodeProjectsAsync();
        public Task<IEnumerable<dynamic>> GetAllUsersAsync();
        public Task<IPagination<RegistrationSummaryResponse>> GetAllUsersAsync(ShowAllUsersDto showAllUsersDto);
        public Task<IEnumerable<dynamic>> TimesheetDetailsbyTimeSheetMasterIdAsync(int timeSheetMasterId);
        public Task<string> GetProjectCodeByIdAsync(int projectId);
        public Task<string> GetProjectNameByIdAsync(int projectId);
        public Task<string> GetUserByIdAsync(int userId);
        public Task<List<ProjectMasterResponse>> ListOfProjectsAsync();
        public Task<List<ProjectMasterResponse>> ListofProjectsByUserAdminAsync(int userId);
        public Task<List<ProjectMasterResponse>> ListofProjectsByUserAsync(int userId);
        public Task<int> GetTotalProjectsCountsAsync();
        public Task<List<AssignedRolesResponse.AdminModel>> ListOfAdminsAsync();
        public Task<List<AssignedRolesResponse.UserModel>> ListOfUserAsync();
        public Task<bool> ValidateAssignationAsync(AssignProjectToUsersDto assignProjectToUsersDto);
        public Task<bool> CheckProjectIdExistsInTimesheetAsync(int projectId);
        public Task<bool> CheckProjectIdExistsInExpenseAsync(int projectId);
        public Task<bool> CheckProjectCodeExistsAsync(string projectCode);
        public Task<bool> CheckProjectNameExistsAsync(string projectName);
        public Task<bool> CheckProjectIdExistsInTimeSheetAsync(int projectId);
        public Task<IPagination<UsersHoursNoCompleteResponse>> GetUsersHoursNoCompleteAsync(UsersHoursNoCompleteDto usersHoursNoCompleteDto);
        public Task<RegistrationViewDetailsResponse> GetUserDetailsByRegistrationIdAsync(int registrationId);
        public Task<RegistrationViewDetailsResponse> GetAdminDetailsByRegistrationIdAsync(int registrationId);
        public Task<int> GetTotalAdminsCountAsync();
        public Task<int> GetTotalUsersCountAsync();
        public Task<IPagination<RegistrationSummaryResponse>> GetAllUsersUnderAdminAsync(ShowAllUsersUnderAdminDto showAllUsersUnderAdminDto);
        public Task<ProjectMasterResponse> GetProjectMasterByIdAsync(int projectId);
        public Task<IPagination<ProjectMasterResponse>> GetAllProjectsAsync(ShowAllProjectsDto showAllProjectsDto);
        public Task<IPagination<NotificationTbResponse>> GetNotificationsTbAsync(ShowNotificationsDto showNotificationsDto);
        public Task<TimeSheetMasterResponse> GetTimeSheetMasterByIdAsync(int timeSheetMasterId);
        public Task<IPagination<TimeSheetMasterResponse>> GetAllTimeSheetsAsync(ShowAllTimeSheetsDto showAllTimeSheetsDto);
        public Task UpdatePasswordAsync(int registrationId, string password);
        public Task UpdateTimeSheetStatusAsync(int timeSheetMasterId, int timeSheetStatus, string? comment);
        public Task UpdateTimeSheetMasterAsync(int timeSheetMasterId, decimal totalHours);
        public Task<TimeSheetDisplayViewResponse> GetTimeSheetsCountByAdminIdAsync(int adminId);
        public Task<TimeSheetDisplayViewResponse> GetTimeSheetsCountByUserIdAsync(int userId);
        public Task<DescriptionTbResponse> GetDescriptionTbAsync(int projectId, int timeSheetMasterId);
        public Task<UserDateIsUsedResponse> GetUsersDateIsUsedAsync(DateTime fromDate, int userId);
        public Task<string> GetStoredPasswordAsync(int registrationId);
        public Task<ExpenseDisplayViewResponse> GetExpesesCountByAdminIdAsync(int adminId);
        public Task<int> GetRegistrationIdByRegistrationUsernameAsync(string userName);
        public Task<int> GetRoleIdbyRolenameAsync(string roleName);
        public Task DisableExistingNotificationsAsync();
        public Task DeActivateNotificationByIdAsync(int notificationId);
    }
}
