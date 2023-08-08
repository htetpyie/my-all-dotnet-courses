using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualBasic;
using System;

namespace HPPMDotNetCore.ExpenseTracker.Features.Expense
{
    public class ExpenseRespModel
    {
        public long ExpenseId { get; set; }
        public long UserId { get; set; }
        public long IncomeId { get; set; }
        public string IncomeName { get; set; }
        public string ExpenseName { get; set; }
        public decimal ExpenseAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpenseDateAsString { get => this.CreatedDate.ToString("MMMM dd yyyy"); }
        public string ExpenseAmountAsString { get => this.ExpenseAmount.ToString("N2"); }
    }
}
