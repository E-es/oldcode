using HR.WebApi.Data.Models;
using HR.WebApi.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HR.WebApi.Controllers
{
    /// <summary>
    /// 组织架构
    /// </summary>
    [RoutePrefix("Org"),Authorize, WebApiTracker]
    public class OrgController : ApiController
    {
        private readonly HrRepository _hrrepository = null;

        public OrgController()
        {
            _hrrepository = new HrRepository();
        }

        // GET: api/org
        /// <summary>
        /// 获取所有组织
        /// </summary>
        /// <returns>组织列表</returns>
        [Route("all")]
        [HttpGet]
        public IEnumerable<Orgunit> GetAll()
        {
            return _hrrepository.GetOrgunits();
        }


        // GET: api/org
        /// <summary>
        /// 根据最后更新时间获取组织
        /// </summary>
        /// <returns>组织列表</returns>
        [Route("time")]
        [HttpGet]
        public IEnumerable<Orgunit> GetAll(DateTime beigin, DateTime end)
        {
            return _hrrepository.GetOrgunits(beigin, end);
        }

        /// <summary>
        /// 根据主键获取组织
        /// </summary>
        /// <param name="value">Id</param>
        /// <returns>组织信息</returns>
        [AllowAnonymous]
        [Route("id/{value}")]
        [HttpGet]
        public Orgunit GetOneById(string value)
        {
            return _hrrepository.GetOrgunitById(value);
        }

        /// <summary>
        /// 根据Hid获取组织
        /// </summary>
        /// <param name="value">Hid</param>
        /// <returns>组织信息</returns>
        [AllowAnonymous]
        [Route("hid/{value}")]
        [HttpGet]
        public Orgunit GetOneByHId(string value)
        {
            return _hrrepository.GetOrgunitByHId(value);
        }

        /// <summary>
        /// 获取该组织下属组织（不包含子部门）
        /// </summary>
        /// <param name="value">Hid</param>
        /// <returns>组织列表</returns>
        [Route("fid/{value}")]
        [HttpGet]
        public IEnumerable<Orgunit> GetChildsByFId(string value)
        {
            return _hrrepository.GetChildsOrgunitsByFid(value);
        }

        /// <summary>
        /// 获取该组织下属组织（包含子部门）
        /// </summary>
        /// <param name="value">Hid</param>
        /// <returns>组织列表</returns>
        [Route("fid/{value}/all")]
        [HttpGet]
        public IEnumerable<Orgunit> GetChildsAllByFId(string value)
        {
            return _hrrepository.GetChildsAllOrgunitsByFid(value);
        }

    }
}
