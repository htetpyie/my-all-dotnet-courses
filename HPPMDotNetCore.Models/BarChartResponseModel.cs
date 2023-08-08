using Newtonsoft.Json;
using System.Collections.Generic;

namespace HPPMDotNetCore.Models
{
    public class BarChartResponseModel
    {
        public List<BarSeryModel> Series { get; set; }
        public List<string> Categories { get; set; }
    }

    public class BarSeryModel
    {
        [JsonProperty(PropertyName = "data")]
        public List<int> Data { get; set; }
    }
}