using Newtonsoft.Json;
using System.Collections.Generic;

namespace HPPMDotNetCore.Models
{
    public class ColumnChartResponseModel
    {
        public List<SeryModel> Series { get; set; }
        public List<string> Categories { get; set; }
    }


}