using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("HR_POSITION")]
    public class Position
    {
        [Key]
        public string C_OID { get; set; }
        public DateTime? C_CREATE_TIME { get; set; }
        public string C_CODE { get; set; }
        public string C_NAME { get; set; }
        [Display(Name ="组织ID")]
        public string C_OU_HID { get; set; }
        public string C_JOB_FAMILY_HID { get; set; }
        public string C_GRADE_ID { get; set; }
        public string C_CLASS_ID { get; set; }
        public DateTime? C_BEGIN_DATE { get; set; }
        public DateTime? C_END_DATE { get; set; }
        public string C_STATUS { get; set; }

        [Display(Name ="职位ID")]
        public string C_HID { get; set; }
        public string C_OLD_HID { get; set; }
        public string C_JOB_MARK { get; set; }
        public string C_OPERATE_TYPE { get; set; }

        public string C_SUPPOSITION_HID { get; set; }

        public DateTime? C_UPDATE_TIME { get; set; }
    }
}
