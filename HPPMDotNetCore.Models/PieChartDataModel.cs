using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPPMDotNetCore.Models
{
    [Table("tbl_PieChart")]
    public class PieChartDataModel
    {
        [Key]
        public int PieChartId { get; set; }
        public string PieChartLabel { get; set; }
        public int PieChartData { get; set; }
    }
}