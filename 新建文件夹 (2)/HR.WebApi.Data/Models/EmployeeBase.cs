using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("EmployeeBase")]
    public class EmployeeBase
    {
        [Key]
        public string EmployeeId { get; set; }
        public string Adcount { get; set; }
        public string UserFullName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string JobId { get; set; }
        public string JobName { get; set; }
        public string JobLevel { get; set; }

        public string JobClassId { get; set; }

        public string ActiveStatus { get; set; }

        public string UserStatus { get; set; }

        [Column("UpdateTime")]
        public DateTime? UpdateTime { get; set; }


        public DateTime? BeginDate { get; set; }
    }
}
