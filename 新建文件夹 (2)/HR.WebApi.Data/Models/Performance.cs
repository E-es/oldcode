using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("HR_EMP_PMG")]
    public class Performance
    {
        [Key]
        public string C_OID { get; set; }
        public string C_EMPLOYEE_ID { get; set; }
        public DateTime? C_CREATE_TIME { get; set; }
        public Int64? C_YEAR { get; set; }
        public string C_PHASENAME { get; set; }
        public DateTime? C_BEGINDATE { get; set; }
        public DateTime? C_ENDDATE { get; set; }
        public decimal C_SCORE { get; set; }
        public string C_RANKING { get; set; }
        public string C_ADUSER { get; set; }
    }
}
