namespace HPPMDotNetCore.ExpenseTracker.Features.User
{
    public class UserReqModel
    {
        public long UserId { get; set; }
        public string RefId{ get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnumRegistrationType UserRegistrationType { get; set; }
    }
}
