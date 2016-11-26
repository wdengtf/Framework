using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Model
{
    public class JsonResult
    {
        private JsonResult()
        {

        }

        public string Message { get; set; }
        public Result Result { get; set; }
        public object Data { get; set; }

        /// <summary>
        /// 返回成功Json
        /// </summary>
        /// <returns></returns>
        public static JsonResult SuccessResult(object data)
        {
            JsonResult re = new JsonResult();
            re.Data = data;
            re.Message = MsgShowConfig.Success;
            re.Result = Result.success;
            return re;
        }

        /// <summary>
        /// 返回失败Json
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static JsonResult FailResult(String Message)
        {
            JsonResult re = new JsonResult();
            re.Data = "";
            re.Message = Message;
            re.Result = Result.fail;
            return re;
        }
    }

    /// <summary>
    /// 结果枚举
    /// </summary>
    public enum Result
    {
        fail,
        success
    }
}
