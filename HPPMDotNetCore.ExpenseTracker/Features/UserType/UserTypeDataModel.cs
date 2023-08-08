using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPPMDotNetCore.ExpenseTracker.Features.UserType
{
    [Table("Tbl_UserType")]
    public class UserTypeDataModel : BaseDataModel
    {
        [Key]
        public long UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public string UserTypeOrder { get; set; }
    }
}
