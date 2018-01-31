using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("HR_EMP_ORG")]
    public class Relation
    {
        [Key]
        public string C_OID { get; set; }
        public DateTime? C_CREATE_TIME { get; set; }
        [Display(Name ="员工ID")]
        public string C_EMPLOYEE_ID { get; set; }
        [Display(Name = "公司ID")]
        public string C_COMPANY_HID { get; set; }
        [Display(Name = "部门ID")]
        public string C_DEPT_HID { get; set; }
        public string C_DEPT_TYPE { get; set; }
        public string C_EMPLOYEE_STATUS { get; set; }
        public string C_POSITION_HID { get; set; }
        public string C_POSITION_TYPE { get; set; }
        public string C_JOBLEVEL { get; set; }
        public string C_ACTIVE_STATUS { get; set; }
        public DateTime? C_BEGIN_DATE { get; set; }
        public DateTime? C_END_DATE { get; set; }
        public DateTime? C_END_DATE_B { get; set; }
        public string C_OPERATE_TYPE { get; set; }
        public DateTime? C_UPDATE_TIME { get; set; }
    }
}