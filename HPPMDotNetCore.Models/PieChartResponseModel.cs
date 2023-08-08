using System.Collections.Generic;

namespace HPPMDotNetCore.Models
{
    public class PieChartResponseModel
    {
        public List<int> Series { get; set; }
        public List<string> Labels { get; set; }
    }
}