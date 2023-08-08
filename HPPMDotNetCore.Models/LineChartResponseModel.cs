using Newtonsoft.Json;
using System.Collections.Generic;

namespace HPPMDotNetCore.Models
{
    public class LineChartResponseModel
    {
        public List<SeryModel> Series { get; set; }
        public List<string> Categories { get; set; }
        public string Curve { get; set; }//'straight','smooth'
    }

    public class SeryModel
    {
        private List<double> data;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "data")]
        public List<double> Data { get => data; set => data = value; }
    }
}