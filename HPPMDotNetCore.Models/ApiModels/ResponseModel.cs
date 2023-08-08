namespace HPPMDotNetCore.Models.ApiModels
{
    public class ResponseModel
    {
        public string RespCode { get; set; }
        public string RespDesp { get; set; }
        public EnumRespType RespType { get; set; }
        public bool IsSuccess
        {
            get
            {
                return RespType == EnumRespType.Success ||
                       RespType == EnumRespType.Information;
            }
        }
        public bool IsError { get => !IsSuccess; }
        public object RespData { get; set; }
    }

    public enum EnumRespType
    {
        Success,
        Information,
        Warning,
        Error,
    }
}
