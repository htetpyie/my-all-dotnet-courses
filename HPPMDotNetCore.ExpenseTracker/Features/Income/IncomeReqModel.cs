namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    public class IncomeReqModel
    {
        public long IncomeId { get; set; }
        public long UserId { get; set; }
        public string IncomeName { get; set; }
        public decimal IncomeAmount { get; set; }
    }
}
