using System.Collections.Generic;

namespace HPPMDotNetCore.ExpenseTracker.Features.Income
{
    public class IncomeListRespModel
    {
        public List<IncomeRespModel> IncomeList { get; set; }
        public PageSetting PageSetting { get; set; }
        public string TotalIncome { get; set; }
    }
}
