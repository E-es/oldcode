using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("HR_ORGUNIT")]
    public class Orgunit
    {
        [Key]
        public string C_OID { get; set; }
        public DateTime? C_CREATE_TIME { get; set; }
        public string C_TYPE { get; set; }
        public string C_CODE { get; set; }
        public string C_NAME { get; set; }
        public string C_ABBREVIATE { get; set; }
        public DateTime? C_EST_DATE { get; set; }
        public string C_LEVEL { get; set; }
        public string C_CONTACT_TEL { get; set; }
        public string C_FAX { get; set; }
        public string C_ADMINISTRATION_AREA { get; set; }
        public string C_ADDRESS { get; set; }
        public string C_POST_CODE { get; set; }
        public string C_INTRODUCTION { get; set; }
        public string C_STATUS { get; set; }

        [Display(Name = "组织ID")]
        public string C_HID { get; set; }
        public string C_OLD_HID { get; set; }
        public string C_SUPERIOR_HID { get; set; }
        public string C_PATH_CODE { get; set; }
        public string C_VIRTUAL_DEPT { get; set; }
        public string C_PIC_ID { get; set; }
        public DateTime? C_BEGIN_DATE { get; set; }
        public DateTime? C_END_DATE { get; set; }
        public string C_REMARK { get; set; }
        [Display(Name = "序号")]
        public Int32? C_DISPLAYNO { get; set; }
        public string C_OPERATE_TYPE { get; set; }

        public DateTime? C_UPDATE_TIME { get; set; }
    }
}
