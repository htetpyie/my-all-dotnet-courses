using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HPPMDotNetCore.Models
{
    [Table("Tbl_ExpenseTracker")]
    public class ExpenseTrackerDataModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public static ExpenseTrackerDataModel Create()
        {          
            return new ExpenseTrackerDataModel 
            {                  
                Date = DateTime.Now,
                Description = "Description Test",
                TransactionType = "Transcation Type Test",
                Amount = 20.22M
            };
        }
    }

    
}
