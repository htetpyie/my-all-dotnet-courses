namespace HPPMDotNetCore.ExpenseTracker.Services
{
    public class LayoutService
    {
        public static string GetDolabLayout()
        {
            return "~/Views/Shared/dolabTheme/_Layout.cshtml";
        }

        public static string GetPagination()
        {
            return "~/Views/Shared/_Pagination.cshtml";
        }
        
        public static string GetComponentPagination()
        {
            return "~/Views/Shared/_ComponentPagination.cshtml";
        }
    }
}