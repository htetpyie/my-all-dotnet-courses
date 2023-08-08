using System;

namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    public class IncomeRespModel
    {
        public long IncomeId { get; set; }
        public long UserId { get; set; }
        public string IncomeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IncomeDateAsString { get => this.CreatedDate.ToString("MMMM dd yyyy"); }
        public decimal IncomeAmount { get; set; }
        public string IncomeAmountAsString { get => this.IncomeAmount.ToString("N2"); }
    }
}
