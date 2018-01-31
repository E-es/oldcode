using HR.WebApi.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.WebApi
{
    public class LoggerHelper
    {
        private static readonly IExtLog loginfo = ExtLogManager.GetLogger("loginfo");
        private static readonly IExtLog logerror = ExtLogManager.GetLogger("logerror");
        private static readonly IExtLog logmonitor = ExtLogManager.GetLogger("dblog");


        #region Monitor
        public static void Error(string ErrorMsg, Exception ex = null)
        {
            if (ex != null)
            {
                logerror.Error(ErrorMsg, ex);
            }
            else
            {
                logerror.Error(ErrorMsg);
            }
        }

        public static void Error(string clientUser, string requestUri, string action, object message, Exception t = null)
        {
            if (t == null)
            {
                logmonitor.Error(GetIP(), clientUser, requestUri, action, message);
            }
            else
            {
                logmonitor.Error(GetIP(), clientUser, requestUri, action, message, t);
            }
        }
        #endregion


        #region Monitor
        public static void Info(string Msg)
        {
            loginfo.Info(Msg);
        }

        public static void Info(string clientUser, string requestUri, string action, object message, Exception t = null)
        {
            if (t == null)
            {
                logmonitor.Info(GetIP(), clientUser, requestUri, action, message);
            }
            else
            {
                logmonitor.Info(GetIP(), clientUser, requestUri, action, message, t);
            }
        }
        #endregion

        #region Monitor
        public static void Monitor(string Msg)
        {
            logmonitor.Info(Msg);
        }

        public static void Monitor(string clientUser, string requestUri, string action, object message, Exception t = null)
        {

            if (t == null)
            {
                logmonitor.Info(GetIP(), clientUser, requestUri, action, message);
            }
            else
            {
                logmonitor.Info(GetIP(), clientUser, requestUri, action, message, t);
            }
           
        }
        #endregion

        public static string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            return ip;
        }
    }
}
