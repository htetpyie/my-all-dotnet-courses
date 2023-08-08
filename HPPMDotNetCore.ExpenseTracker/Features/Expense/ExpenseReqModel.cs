namespace HPPMDotNetCore.ExpenseTracker.Features.Expense
{
    public class ExpenseReqModel
    {
        public long ExpenseId { get; set; }
        public long IncomeId { get; set; }
        public string IncomeName { get; set; }
        public long UserId { get; set; }
        public string ExpenseName { get; set; }
        public decimal ExpenseAmount { get; set; }
    }
}
