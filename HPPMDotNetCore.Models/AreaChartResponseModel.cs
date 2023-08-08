using System.Collections.Generic;

namespace HPPMDotNetCore.Models
{
    public class AreaChartResponseModel
    {
        public List<SeryModel> Series { get; set; }
        public List<string> Categories { get; set; }
    }
}