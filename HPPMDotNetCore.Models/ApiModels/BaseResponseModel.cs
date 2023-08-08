using RepoDb.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HPPMDotNetCore.Models.ApiModels
{
    public class BaseResponseModel
    {
        public ResponseModel Resopnse { get; set; }       

        public static ResponseModel GetSuccess(object respData = default, string message = null)
        {
            return new ResponseModel
            {
                RespCode = "I0001",
                RespDesp = message.IsNullOrEmpty() ? "Success" : message,
                RespType = EnumRespType.Success,
                RespData = respData
            };
        }
        public static ResponseModel GetError(Exception ex = null,
            string methodName = null, 
            string filePath = null)
        {
            string message = $@"FileName : {Path.GetFileNameWithoutExtension(filePath)} | 
                               MethodName : {methodName} |
                               Exception : {ex.ToString()} ";
            return new ResponseModel
            {
                RespCode = "E0001",
                RespDesp = message,
                RespType = EnumRespType.Error
            };
        }
    }
}
