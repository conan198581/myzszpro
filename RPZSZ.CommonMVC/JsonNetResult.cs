using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;
using System.Web;

namespace RPZSZ.CommonMVC
{
    public class JsonNetResult : JsonResult
    {
        //定义json序列化的配置的 属性
        public JsonSerializerSettings Settings { get; private set; }

        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings {
                ReferenceLoopHandling=ReferenceLoopHandling.Ignore,//忽略循环引用
                DateFormatString="yyyy-MM-dd HH:mm:ss",//日期格式化
                ContractResolver=new CamelCasePropertyNamesContractResolver()////json中属性开头字母小写的驼峰命名
            };
        }

        //重写原生的jsonresult的ExecuteResult
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;

            var scriptSerializer = JsonSerializer.Create(this.Settings);
            scriptSerializer.Serialize(response.Output, this.Data);
        }
    }
}
