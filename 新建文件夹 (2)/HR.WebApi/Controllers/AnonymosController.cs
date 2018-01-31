using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HR.WebApi.Data.Models;
using HR.WebApi.Repositorys;

namespace HR.WebApi.Controllers
{
    [WebApiTracker]
    [RoutePrefix("Ext"),AllowAnonymous]
    public class AnonymosController : ApiController
    {
        private readonly HrRepository _hrrepository = null;

        public AnonymosController()
        {
            _hrrepository = new HrRepository();
        }

        [HttpGet, Route("Cards")]
        public IEnumerable<BankCards> GetCards(string value)
        {
            if (value.Length < 20 && value.Length > 2)
            {
                value = _hrrepository.GetEmployeeByAd(value).C_OID;
            }
            return _hrrepository.GetBankCardsByUserId(value);
        }

        [HttpGet, Route("KPI")]
        public IEnumerable<Performance> GetPerformance(string value,int year)
        {
            if (value.Length < 20 && value.Length > 2)
            {
                value = _hrrepository.GetEmployeeByAd(value).C_OID;
            }
            return _hrrepository.GetPerformancesByUserId(value, year);
        }

        [HttpGet, Route("KPIHalf")]
        public IEnumerable<Performance> GetTop6Performance(string value ,DateTime end)
        {
            if (value.Length < 20 && value.Length > 2)
            {
                value = _hrrepository.GetEmployeeByAd(value).C_OID;
            }
            return _hrrepository.GetPerformancesTop6ByUserId(value, end);
        }
    }
}
