using HPPMDotNetCore.ExpenseTracker.Features.Dashboard;

namespace HPPMDotNetCore.ExpenseTracker.Features.SignUp
{
    public class MessageResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
    }

    public class Base
    {
        private static IDashboardService _iDashboardService;

        public Base(IDashboardService iDashboardService)
        {
            _iDashboardService = iDashboardService;
        }

        public static MessageResponseModel GetSuccess
            (string message, string url = default, object data = default)
        {
            return new MessageResponseModel
            {
                IsSuccess = true,
                Message = message,
                Url = url,
                Data = data
            };
        }

        public static MessageResponseModel GetError(string message)
        {
            return new MessageResponseModel
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}