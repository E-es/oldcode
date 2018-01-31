using System.Collections.Generic;
using System.Web.Http;
using HR.WebApi.Repositorys;
using HR.WebApi.Data.Models;
using System.Security.Claims;
using System;

namespace HR.WebApi.Controllers
{
    /// <summary>
    /// 人员信息
    /// </summary>
    [RoutePrefix("Employees"),Authorize, WebApiTracker]
    public class EmployeesController : ApiController
    {

        private readonly HrRepository _hrrepository = null;
        /// <summary>
        /// 
        /// </summary>
        public EmployeesController()
        {
            _hrrepository = new HrRepository();
        }


        #region base info

        /// <summary>
        /// 查询是否有更新
        /// </summary>
        [Route("IsUpdated")]
        [HttpGet]
        public bool IsUpdated(string id,string timestamp)
        {
            return _hrrepository.IsUpdated(id, timestamp);
        }

        /// <summary>
        /// 获取所有员工基础信息（全部同步使用）
        /// </summary>
        /// <returns>员工列表</returns>
        /// <remarks>GET:Employees</remarks>
        [Route("all")]
        [HttpGet]
        public IEnumerable<EmployeeBase> GetEmployeeBaseAll()
        {
            return _hrrepository.GetEmployeeBase((User.Identity as ClaimsIdentity).FindFirst("rang").Value, null, null);
        }

        /// <summary>
        /// 根据最后更新时间获取基础信息（增量同步使用）
        /// </summary>
        /// <returns>员工列表</returns>
        /// <remarks>GET: Employees/time</remarks>
        [Route("time")]
        [HttpGet]
        public IEnumerable<EmployeeBase> GetEmployeeBaseAll(DateTime beigin, DateTime end)
        {
            return _hrrepository.GetEmployeeBase((User.Identity as ClaimsIdentity).FindFirst("rang").Value, beigin, end);
        }



        /// <summary>
        /// 根据账号获取员工基础信息
        /// </summary>
        /// <returns>员工列表</returns>
        /// <remarks>GET: Employees/lir</remarks>
        [Route("{value}")]
        [HttpGet]
        public IEnumerable<EmployeeBase> GetEmployeeBaseByAd(string value)
        {
            if (value == "all")
            {
                throw new System.Exception();
            }
            return _hrrepository.GetOneEmployeeBase(value);
        }

        #endregion


        /// <summary>
        /// 获取所有员工信息
        /// </summary>
        /// <returns>员工列表</returns>
        /// <remarks>GET: Employees/detail/all</remarks>
        [Route("detail/all")]
        [HttpGet]
        //[Authorize(Users = "test")]
        public IEnumerable<Employee> GetAll()
        {
            string rang = (User.Identity as ClaimsIdentity).FindFirst("rang").Value;
            return rang.Contains("Anonymous") ? _hrrepository.GetEmployees() : GetListAllbyHid(rang);
        }


        /// <summary>
        /// 根据最后更新时间获取员工信息
        /// </summary>
        /// <returns>员工列表</returns>
        /// <remarks>GET: Employees/detail/all</remarks>
        [Route("detail/time")]
        [HttpGet]
        //[Authorize(Users = "test")]
        public IEnumerable<Employee> GetAll(DateTime beigin, DateTime end)
        {
            string rang = (User.Identity as ClaimsIdentity).FindFirst("rang").Value;
            return rang.Contains("Anonymous") ? _hrrepository.GetEmployees(beigin, end) : _hrrepository.GetListAllbyHid(rang, beigin, end);
        }


        /// <summary>
        /// 根据主键获取员工
        /// </summary>
        /// <param name="value">Id</param>
        /// <returns>员工信息</returns>
        /// <remarks>GET:Employees/id/0094e1b9a39e4f76b91883c846823600</remarks>
        [Route("id/{value}")]
        [HttpGet]
        public Employee GetOneById(string value)
        {
            return _hrrepository.GetEmployeeById(value);
        }

        /// <summary>
        /// 根据AD获取员工
        /// </summary>
        /// <param name="value">AD</param>
        /// <returns>员工信息</returns>
        /// <remarks>GET:Employees/ad/value</remarks>
        [Route("ad/{value}")]
        [HttpGet]
        public Employee GetOneByAd(string value)
        {
            return _hrrepository.GetEmployeeByAd(value);
        }

        /// <summary>
        /// 获取该部门下所有员工（不包含子部门）
        /// </summary>
        /// <param name="value">部门ID</param>
        /// <returns>员工列表</returns>
        /// <remarks>GET:Employees/hid/value</remarks>
        [Route("hid/{value}")]
        [HttpGet]
        public IEnumerable<Employee> GetListbyHid(string value)
        {
            return _hrrepository.GetListbyHid(value);
        }

        /// <summary>
        /// 获取该部门下所有员工（包含子部门）
        /// </summary>
        /// <param name="value">部门ID</param>
        /// <returns>员工列表</returns>
        /// <remarks>GET:Employees/hid/value/all</remarks>
        [Route("hid/{value}/all")]
        [HttpGet]
        public IEnumerable<Employee> GetListAllbyHid(string value)
        {
            return _hrrepository.GetListAllbyHid(value);
        }

        /// <summary>
        /// 根据AD账号查询岗位信息（多条记录）
        /// </summary>
        /// <param name="value">Ad账号</param>
        /// <returns>岗位信息</returns>
        [Route("postions/{value}")]
        [HttpGet]
        public IEnumerable<Position> GetPostionsByAd(string value)
        {
            return _hrrepository.GetPostionsByAd(value);
        }

        /// <summary>
        /// 根据JobId查询岗位信息（单条记录）
        /// </summary>
        /// <param name="value">JobId</param>
        /// <returns>岗位信息</returns>
        [Route("postion/{value}")]
        [HttpGet]
        public Position GetPostionsById(string value)
        {
            return _hrrepository.GetPostionsById(value);
        }

        /// <summary>
        /// 查询所有岗位信息
        /// </summary>
        /// <returns>岗位信息</returns>
        [Route("postions/all")]
        [HttpGet]
        //[AllowAnonymous]
        public IEnumerable<Position> GetPostionsAll()
        {
            return _hrrepository.GetPostions();
        }


        /// <summary>
        /// 根据Id查询拓展信息（单条记录）
        /// </summary>
        /// <param name="value">ADcount/EmployeeId</param>
        /// <returns>拓展信息</returns>
        [Route("expand/{value}")]
        [AllowAnonymous]
        [HttpGet]
        public EmployeeExpand GetExpandById(string value)
        {
            return _hrrepository.GetExpandById(value);
        }

        #region NonAction
        /// <summary>
        /// NULL
        /// </summary>
        /// <param name="value"></param>
        [NonAction]
        public void Post([FromBody]string value)
        {
        }
        /// <summary>
        /// NULL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [NonAction]
        public void Put(int id, [FromBody]string value)
        {
        }
        /// <summary>
        /// NULL
        /// </summary>
        /// <param name="id"></param>
        [NonAction]
        public void Delete(int id)
        {
        }

        #endregion
    }
}
