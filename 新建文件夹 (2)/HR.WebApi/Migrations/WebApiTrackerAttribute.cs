using HR.WebApi.Data.Extensions;
using System;
using System.IO;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HR.WebApi
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class WebApiTrackerAttribute : ActionFilterAttribute
    {
        private readonly string Key = "_thisWebApiOnActionMonitorLog_";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);
            WebApiMonitorLog MonLog = new WebApiMonitorLog();
            MonLog.UserName = actionContext.RequestContext.Principal.Identity.IsAuthenticated ? actionContext.RequestContext.Principal.Identity.Name : "Anonymous";
            MonLog.ExecuteStartTime = DateTime.Now;
            //获取Action 参数
            MonLog.ActionParams = actionContext.ActionArguments;
            MonLog.HttpRequestHeaders = actionContext.Request.Headers.ToString();
            MonLog.HttpMethod = actionContext.Request.Method.Method;
            MonLog.ActionUri = actionContext.Request.RequestUri;

            actionContext.Request.Properties[Key] = MonLog;
            var form = System.Web.HttpContext.Current.Request.Form;
            Stream stream = actionContext.Request.Content.ReadAsStreamAsync().Result;
            Encoding encoding = Encoding.UTF8;
            string responseData = "";
            using (StreamReader reader = new StreamReader(stream, encoding))
            {
                responseData = reader.ReadToEnd().ToString();
            }
            if (!string.IsNullOrWhiteSpace(responseData) && !MonLog.ActionParams.ContainsKey("__EntityParamsList__"))
            {
                MonLog.ActionParams["__EntityParamsList__"] = responseData;
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            WebApiMonitorLog MonLog = actionExecutedContext.Request.Properties[Key] as WebApiMonitorLog;
            MonLog.ExecuteEndTime = DateTime.Now;
            MonLog.ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            MonLog.ControllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            LoggerHelper.Monitor(MonLog.UserName,MonLog.ActionUri.ToString(),MonLog.ActionName,MonLog.GetLoginfo());
            if (actionExecutedContext.Exception != null)
            {
                LoggerHelper.Error(MonLog.UserName, MonLog.ActionUri.ToString(), MonLog.ActionName, MonLog.GetLoginfo(), actionExecutedContext.Exception);
            }
        }
    }
}
