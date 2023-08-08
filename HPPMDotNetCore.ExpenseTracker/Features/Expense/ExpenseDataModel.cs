using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPPMDotNetCore.ExpenseTracker.Features.Expense
{
    [Table("Tbl_Expense")]
    public class ExpenseDataModel : BaseDataModel
    {
        [Key]
        public long ExpenseId { get; set; }
        public long IncomeId { get; set; }
        public long UserId { get; set; }
        public string ExpenseName { get; set; }
        public decimal ExpenseAmount { get; set; }
    }
}
