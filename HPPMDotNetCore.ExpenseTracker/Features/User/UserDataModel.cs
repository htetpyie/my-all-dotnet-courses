using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace HPPMDotNetCore.ExpenseTracker.Features.User
{
    [Table("Tbl_User")]
    public class UserDataModel : BaseDataModel
    {
        [Key]
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRegistrationStatus { get; set; }
    }
}
