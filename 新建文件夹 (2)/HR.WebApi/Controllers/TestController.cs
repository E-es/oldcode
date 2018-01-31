using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.WebApi.Controllers
{
    public class TestController : ApiController
    {

        /// <summary>
        /// 接口测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("~/Hello"), AllowAnonymous]
        public string SayHello()
        {
            return "Hello, 恭喜您接口连接正常！";
        }
    }
}
