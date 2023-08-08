using System;

namespace HPPMDotNetCore.ExpenseTracker
{
    public class BaseDataModel
    {
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
