using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    [Table("Tbl_Income")]
    public class IncomeDataModel : BaseDataModel
    {
        [Key]
        public long IncomeId { get; set; }
        public long UserId { get; set; }
        public string IncomeName { get; set; }
        public decimal IncomeAmount { get; set; }

    }
}
