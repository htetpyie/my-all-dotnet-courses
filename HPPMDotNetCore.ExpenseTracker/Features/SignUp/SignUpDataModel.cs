using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPPMDotNetCore.ExpenseTracker.Features.SignUp
{
    [Table("Tbl_SignUp")]
    public class SignUpDataModel : BaseDataModel
    {
        [Key]
        public string RefId { get; set; }
        public long UserId { get; set; }
        public bool IsConfirmed { get; set; }
    }

    public class SignUpReqModel
    {
		public long UserId { get; set; }
		public string RefId { get; set; }
		public bool IsConfirmed { get; set; }
	}

	public class SignUpRespModel
	{
		public long UserId { get; set; }
		public long RefId { get; set; }
		public bool IsConfirmed { get; set; }
	}

	public class SignInReqModel
	{
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
