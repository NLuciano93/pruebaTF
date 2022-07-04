using Fusap.Common.Model;

namespace Fusap.TimeSheet.Application.Notifications
{
    public static class ErrorCatalog
    {
        public static class TimeSheet
        {
            public static ErrorCatalogEntry ErrorDuringDataProcessing =>
                (
                "TIMESHEET-000",
                "Error during data processing"
                );

            public static ErrorCatalogEntry InvalidModel =>
                (
                "TIMESHEET-001",
                "Invalid model"
                );

            public static ErrorCatalogEntry TotalHoursNegative =>
                (
                "TIMESHEET-002",
                "TotalHours cannot be negative"
                );

            public static ErrorCatalogEntry Exception =>
                (
                "TIMESHEET-003",
                "Internal Server Error"
                );

            public static ErrorCatalogEntry TokenInvalidCredentials =>
                (
                "TIMESHEET-004",
                "Invalid credentials"
                );

            public static ErrorCatalogEntry UserDoesNotExist =>
                (
                "TIMESHEET-005",
                "User does not exist"
                );

            public static ErrorCatalogEntry InvalidOffset =>
                (
                "TIMESHEET-006",
                "Offset must be greater to zero"
                );

            public static ErrorCatalogEntry UndefinedName =>
                (
                "TIMESHEET-007",
                "Undefined or empty Name"
                );

            public static ErrorCatalogEntry InvalidNameLength =>
                (
                "TIMESHEET-008",
                "Name parameter must be between 1 and 100 chars (inclusive)"
                );

            public static ErrorCatalogEntry UndefinedMobileno =>
                (
                "TIMESHEET-009",
                "Undefined or empty Mobileno"
                );

            public static ErrorCatalogEntry InvalidMobileno =>
                (
                "TIMESHEET-010",
                "Invalid Mobileno"
                );

            public static ErrorCatalogEntry UndefinedEmailId =>
                (
                "TIMESHEET-011",
                "Undefined or empty EmailId"
                );

            public static ErrorCatalogEntry InvalidEmailId =>
                (
                "TIMESHEET-012",
                "Invalid EmailId"
                );

            public static ErrorCatalogEntry UndefinedPassword =>
                (
                "TIMESHEET-013",
                "Undefined or empty Name"
                );

            public static ErrorCatalogEntry InvalidPasswordLength =>
                (
                "TIMESHEET-014",
                "Password parameter must be between 6 and 7 chars"
                );

            public static ErrorCatalogEntry UndefinedUsername =>
                (
                "TIMESHEET-015",
                "Undefined or empty Username"
                );

            public static ErrorCatalogEntry InvalidUsernameLength =>
                (
                "TIMESHEET-016",
                "Invalid Username Length"
                );

            public static ErrorCatalogEntry PasswordsDidNotMatch =>
                (
                "TIMESHEET-017",
                "Passwords did not match"
                );

            public static ErrorCatalogEntry UsernameAlreadyExists =>
                (
                "TIMESHEET-018",
                "Username already exists"
                );

            public static ErrorCatalogEntry HaveNotSelectedUsersToAssign =>
                (
                "TIMESHEET-019",
                "You have not selected users to assign"
                );

            public static ErrorCatalogEntry ThereAreNoUsersToAssign =>
                (
                "TIMESHEET-020",
                "There are no users to assign"
                );

            public static ErrorCatalogEntry DescriptionTbDoesNotExist =>
                (
                "TIMESHEET-021",
                "DescriptionTb does not exist"
                );

            public static ErrorCatalogEntry UserHasAlreadyBeenAssigned =>
                (
                "TIMESHEET-022",
                "The user has already been assigned to this project"
                );

            public static ErrorCatalogEntry ProjectNameIsNull =>
                (
                "TIMESHEET-023",
                "ProjectName is Null"
                );

            public static ErrorCatalogEntry ProjectNameIsEmpty =>
                (
                "TIMESHEET-024",
                "The user has already been assigned to this project"
                );

            public static ErrorCatalogEntry ProjectIsInUse =>
                (
                "TIMESHEET-025",
                "Project is in use and cannot be deleted"
                );

            public static ErrorCatalogEntry DateOutOfRange =>
                (
                "TIMESHEET-026",
                "Date out of range. Date must be greater than 01/01/2020"
                );

            public static ErrorCatalogEntry RegistrationIdNegative =>
                (
                "TIMESHEET-027",
                "RegistrationId cannot be negative"
                );

            public static ErrorCatalogEntry InvalidName =>
                (
                "TIMESHEET-028",
                "Undefined or empty Name"
                );

            public static ErrorCatalogEntry InvalidSortDirection =>
                (
                "TIMESHEET-029",
                "Invalid SortDirection. This value can only be ASC or DESC"
                );

            public static ErrorCatalogEntry InvalidSortColumn =>
                (
                "TIMESHEET-030",
                "Invalid SortColumn"
                );

            public static ErrorCatalogEntry InvalidProjectId =>
                (
                "TIMESHEET-031",
                "Invalid ProjectId"
                );

            public static ErrorCatalogEntry ProjectDoesNotExist =>
                (
                "TIMESHEET-032",
                "Project does not exist"
                );

            public static ErrorCatalogEntry InvalidTimeSheetMasterId =>
                (
                "TIMESHEET-033",
                "Invalid TimeSheetMasterId"
                );

            public static ErrorCatalogEntry TimeSheetMasterDoesNotExist =>
                (
                "TIMESHEET-034",
                "TimeSheetMaster does not exist"
                );

            public static ErrorCatalogEntry InvalidPurposeorReasonLength =>
                (
                "TIMESHEET-035",
                "Invalid PurposeorReason Length, must be less than 50 chars"
                );

            public static ErrorCatalogEntry InvalidPurposeorReason =>
                (
                "TIMESHEET-036",
                "Invalid PurposeorReason"
                );

            public static ErrorCatalogEntry InvalidVoucherIdLength =>
                (
                "TIMESHEET-037",
                "Invalid VoucherId Length, must be less than 50 chars"
                );

            public static ErrorCatalogEntry InvalidVoucherId =>
                (
                "TIMESHEET-038",
                "Invalid VoucherId"
                );

            public static ErrorCatalogEntry InvalidCommentLength =>
                (
                "TIMESHEET-039",
                "Invalid Comment Length, must be less than 100 chars"
                );

            public static ErrorCatalogEntry InvalidComment =>
                (
                "TIMESHEET-040",
                "Invalid Comment"
                );

            public static ErrorCatalogEntry InvalidUserId =>
                (
                "TIMESHEET-041",
                "Invalid UserId"
                );

            public static ErrorCatalogEntry UndefinedComment =>
                (
                "TIMESHEET-042",
                "Undefined or empty Comment"
                );

            public static ErrorCatalogEntry DescriptionIsNull =>
                (
                "TIMESHEET-043",
                "Description is Null"
                );

            public static ErrorCatalogEntry UndefinedDescription =>
                (
                "TIMESHEET-044",
                "Undefined or empty Description"
                );

            public static ErrorCatalogEntry InvalidDescriptionLength =>
                (
                "TIMESHEET-045",
                "Invalid Description Length, must be less than 100 chars"
                );

            public static ErrorCatalogEntry InvalidStatus =>
                (
                "TIMESHEET-046",
                "Status must be between 1 and 3"
                );

            public static ErrorCatalogEntry DateIsNotUsedForUserId =>
                (
                "TIMESHEET-047",
                "Date is not used for UserId"
                );

            public static ErrorCatalogEntry ProjectCodeDoesNotExist =>
                (
                "TIMESHEET-048",
                "ProjectCode does not exist"
                );

            public static ErrorCatalogEntry ProjectCodeIsNull =>
                (
                "TIMESHEET-049",
                "ProjectCode is Null"
                );

            public static ErrorCatalogEntry ProjectCodeIsEmpty =>
                (
                "TIMESHEET-050",
                "ProjectCode is Empty"
                );

            public static ErrorCatalogEntry InvalidProjectCodeLength =>
                (
                "TIMESHEET-051",
                "Invalid ProjectCode length"
                );

            public static ErrorCatalogEntry InvalidProjectNameLength =>
                (
                "TIMESHEET-052",
                "Invalid ProjectName length"
                );

            public static ErrorCatalogEntry ProjectNameDoesNotExist =>
                (
                "TIMESHEET-053",
                "ProjectName does not exist"
                );

            public static ErrorCatalogEntry RegistrationDoesNotExist =>
                (
                "TIMESHEET-054",
                "Registration does not exist"
                );

            public static ErrorCatalogEntry InvalidRoleNameLength =>
                (
                "TIMESHEET-055",
                "Invalid RoleName length"
                );

            public static ErrorCatalogEntry RoleNameIsNull =>
                (
                "TIMESHEET-056",
                "ProjectCode is Null"
                );

            public static ErrorCatalogEntry RoleNameIsEmpty =>
                (
                "TIMESHEET-057",
                "ProjectCode is Empty"
                );

            public static ErrorCatalogEntry RoleDoesNotExist =>
                (
                "TIMESHEET-058",
                "Role does not exist"
                );

            public static ErrorCatalogEntry InvalidMessageLength =>
                (
                "TIMESHEET-059",
                "Invalid Message Length, must be less than 50 chars"
                );

            public static ErrorCatalogEntry InvalidMessage =>
                (
                "TIMESHEET-060",
                "Undefined or empty Message"
                );

            public static ErrorCatalogEntry InvalidNotificationId =>
               (
               "TIMESHEET-061",
               "Invalid NotificationId"
               );
        }
    }
}
