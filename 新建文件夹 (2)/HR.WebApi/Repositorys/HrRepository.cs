using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HR.WebApi.Data.Contexts;
using HR.WebApi.Data.Models;
using System.Data.SqlClient;

namespace HR.WebApi.Repositorys
{
    public class HrRepository : IDisposable
    {

        #region Method Employees  
        public IEnumerable<Employee> GetEmployees()
        {
            var result = _hrdbcontext.Employees.ToList().Where(o=> o.C_OPERATE_TYPE != "3");

            return result;
        }
        internal IEnumerable<Employee> GetEmployees(DateTime beigin, DateTime end)
        {
            var result = _hrdbcontext.Employees.Where(o => o.C_OPERATE_TYPE != "3").Where(s => s.C_UPDATE_TIME >= beigin & s.C_UPDATE_TIME <= end);

            return result;
        }
        public Employee GetEmployeeById(string _c_oid)
        {
            var result = _hrdbcontext.Employees.Where(o => o.C_OPERATE_TYPE != "3").SingleOrDefault<Employee>(s => s.C_OID ==_c_oid);

            return result;
        }
        public Employee GetEmployeeByAd(string _adcount)
        {
            var result = _hrdbcontext.Employees.Where(o => o.C_OPERATE_TYPE != "3").SingleOrDefault<Employee>(s => s.C_ADCODE == _adcount);

            return result;
        }

        internal bool IsUpdated(string _id, string _timestamp)
        {
            var result = _hrdbcontext.Employees.Where(o => o.C_OPERATE_TYPE != "3").SingleOrDefault<Employee>(s => s.C_OID == _id);

            return result.TIMESTAMPS != _timestamp;
        }

        internal IEnumerable<Employee> GetListbyHid(string _value)
        {
            #region string sql

            string _sql = @"select * from HR_EMPLOYEE e
where exists(
select * from HR_EMP_ORG g
where C_DEPT_HID=@HID and e.C_OID=g.C_EMPLOYEE_ID and e.C_OPERATE_TYPE <> '3')";
            SqlParameter[] _sqlParameters = { new SqlParameter { ParameterName = "HID", Value = _value } };

            #endregion

            var result = _hrdbcontext.Employees.SqlQuery(_sql, _sqlParameters);

            return result;
        }

        internal IEnumerable<Employee> GetListAllbyHid(string _value)
        {
            #region string sql

            string strwhere = string.Empty;
            if (_value.Contains(","))
            {
                int i = 0;
                foreach (var item in _value.Split(','))
                {
                    strwhere += string.Format("{1} o.C_PATH_CODE like '%{0}%'", item, i > 0 ? "or" : "");
                    i++;
                }
            }
            else
                strwhere = string.Format("o.C_PATH_CODE like '%{0}%'", _value);

            string _sql = @"select * from HR_EMPLOYEE e
where exists
(
select 1 from HR_EMP_ORG g
where e.C_OID=g.C_EMPLOYEE_ID
and exists (
	select 1 from HR_ORGUNIT o
	where o.C_HID = g.C_DEPT_HID
	and ({0})
)
)";
            #endregion

            var result = _hrdbcontext.Employees.SqlQuery(string.Format(_sql, strwhere));

            return result;
        }



        internal IEnumerable<Employee> GetListAllbyHid(string rang, DateTime beigin, DateTime end)
        {
            #region string sql
            string strupdata,strwhere = string.Empty;

            if (beigin > end)
            {
                throw new Exception("参数不正确");
            }

            strupdata = string.Format(@"e.C_UPDATE_TIME >= '{0}' and e.C_UPDATE_TIME <= '{1}' and", beigin, end);

            if (rang.Contains(","))
            {
                int i = 0;
                foreach (var item in rang.Split(','))
                {
                    strwhere += string.Format("{1} o.C_PATH_CODE like '%{0}%'", item, i > 0 ? "or" : "");
                    i++;
                }
            }
            else
                strwhere = string.Format("o.C_PATH_CODE like '%{0}%'", rang);

            string _sql = @"select * from HR_EMPLOYEE e
where e.C_OPERATE_TYPE <> '3' and {1} exists
(
select 1 from HR_EMP_ORG g
where e.C_OID=g.C_EMPLOYEE_ID
and exists (
	select 1 from HR_ORGUNIT o
	where o.C_HID = g.C_DEPT_HID
	and ({0})
)
)";
            #endregion

            var result = _hrdbcontext.Employees.SqlQuery(string.Format(_sql, strwhere, strupdata));

            return result;
        }


        internal IEnumerable<Position> GetPostionsByAd(string _value)
        {
            #region string  sql

            string _sql = @"select * from  [dbo].[HR_POSITION] p
where exists
( 
select * from [dbo].[HR_EMP_ORG] o
where p.C_HID = o.C_POSITION_HID
and exists
(
	select * from [dbo].[HR_EMPLOYEE] e
	where o.C_EMPLOYEE_ID = e.C_OID
	and e.C_ADCODE = '{0}'
    and e.C_OPERATE_TYPE <> '3'
)
)";
            #endregion

            var result = _hrdbcontext.Positions.SqlQuery(string.Format(_sql, _value));

            return result;
        }

        internal Position GetPostionsById(string _value)
        {
            var result = _hrdbcontext.Positions.SingleOrDefault<Position>(o => o.C_OID == _value);

            return result;
        }

        internal IEnumerable<Position> GetPostions()
        {
            var result = _hrdbcontext.Positions.ToList();

            return result;
        }


        internal IEnumerable<EmployeeBase> GetEmployeeBase()
        {

            var result = _hrdbcontext.EmployeeBases.ToList();

            return result;
        }

        internal IEnumerable<EmployeeBase> GetEmployeeBase(string _rang, DateTime? beigin, DateTime? end)
        {
            #region  sql  string
            string strwhere = string.Empty, strupdata = string.Empty;
            end = end == null ? new DateTime(2999, 12, 30) : end;
            beigin = beigin == null ? new DateTime(1900, 1, 1) : beigin;
            if (beigin > end)
            {
                throw new Exception("参数不正确");
            }
            strupdata = string.Format(@" UpdateTime >= '{0}' and UpdateTime <= '{1}'", beigin, end);

            if (!_rang.Contains("Anonymous"))
            {
                if (_rang.Contains(","))
                {
                    int i = 0;
                    strwhere = "(";
                    foreach (var item in _rang.Split(','))
                    {
                        strwhere += string.Format("{1} Departmentpath like '%{0}%'", item, i > 0 ? "or" : "");
                        i++;
                    }

                    strwhere = strwhere + ")";
                }
                else
                    strwhere = string.Format("Departmentpath like '%{0}%'", _rang);
            }

            string _sql = @"SELECT * FROM [HRDB].[dbo].[EmployeeBase] WHERE {0} {1}";

            strwhere = !string.IsNullOrEmpty(strupdata) && !string.IsNullOrEmpty(strwhere) ? " and " + strwhere : strwhere;

            #endregion

            var result = _hrdbcontext.EmployeeBases.SqlQuery(string.Format(_sql, strupdata, strwhere));

            return result;
        }


        internal IEnumerable<EmployeeBase> GetOneEmployeeBase(string _value)
        {
            var result = _hrdbcontext.EmployeeBases.Where(o => o.Adcount == _value);

            return result;
        }

        internal bool IsHaveRight(string _ad)
        {


            return false;
        }

        #endregion


        #region Method Orgunits
        public List<Orgunit> GetOrgunits()
        {
            var result = _hrdbcontext.Orgunits.ToList();

            return result;
        }

        internal IEnumerable<Orgunit> GetOrgunits(DateTime beigin, DateTime end)
        {
            var result = _hrdbcontext.Orgunits.Where(s => s.C_UPDATE_TIME >= beigin & s.C_UPDATE_TIME <= end);

            return result;
        }

        public Orgunit GetOrgunitById(string _c_oid)
        {
            var result = _hrdbcontext.Orgunits.SingleOrDefault<Orgunit>(s => s.C_OID == _c_oid);

            return result;
        }

        public Orgunit GetOrgunitByHId(string _hid)
        {
            var result = _hrdbcontext.Orgunits.SingleOrDefault<Orgunit>(s => s.C_HID == _hid);

            return result;
        }

        public List<Orgunit> GetChildsOrgunitsByFid(string _hid)
        {
            var result = _hrdbcontext.Orgunits.Where(s => s.C_SUPERIOR_HID == _hid).ToList();

            return result;
        }

        public List<Orgunit> GetChildsAllOrgunitsByFid(string _hid)
        {
            var result = _hrdbcontext.Orgunits.Where(s => s.C_PATH_CODE.Contains(_hid)).ToList();

            return result;
        }



        public EmployeeExpand GetExpandById(string _value)
        {
            var result = _hrdbcontext.EmployeeExpands.Where(s => s.Years == DateTime.Now.Year.ToString()).FirstOrDefault(s => s.EmployeeId == _value || s.Adcount == _value);

            return result;
        }



        #endregion


        #region Ext

        public IEnumerable<BankCards> GetBankCardsByUserId(string userId)
        {
            var result = _hrdbcontext.BankCard.Where(o => o.EmployeeId == userId);
            return result;
        }

        public IEnumerable<Performance> GetPerformancesByUserId(string userId,int year)
        {
            var result = _hrdbcontext.Performances.Where(o => o.C_EMPLOYEE_ID == userId).Where(o => o.C_YEAR == year).OrderByDescending(o => o.C_CREATE_TIME);
            return result;
        }

        public IEnumerable<Performance> GetPerformancesTop6ByUserId(string userId,DateTime end)
        {
            var result = _hrdbcontext.Performances.Where(o => o.C_EMPLOYEE_ID == userId & o.C_SCORE > 50 & o.C_CREATE_TIME <= end).OrderByDescending(o => o.C_CREATE_TIME).Take(6);
            return result;
        }

        #endregion




        #region Pository

        private HRDBContext _hrdbcontext = null;

        public HrRepository()
        {
            _hrdbcontext = new HRDBContext();
        }

        public void Dispose()
        {
            _hrdbcontext.Dispose();
        }

        #endregion
    }
}