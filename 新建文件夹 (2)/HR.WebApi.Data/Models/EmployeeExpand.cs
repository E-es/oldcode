using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("EmployeeExpand")]
    public class EmployeeExpand
    {
        [Key]
        public string EmployeeId { get; set; }
        public string UserCode { get; set; }
        public string UserFullName { get; set; }
        public string Adcount { get; set; }
        public string Years { get; set; }
        public string AnnualVacation { get; set; }
        public string BankCard { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
