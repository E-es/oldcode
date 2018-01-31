using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("HR_EMPLOYEE")]
    public class Employee
    {
        [Key]
        public string C_OID { get; set; }

        public string C_OLD_OID { get; set; }
        public DateTime? C_CREATE_TIME { get; set; }
        public string C_CODE { get; set; }
        public string C_ADCODE { get; set; }

        [Required]
        public string C_NAME { get; set; }
        public string C_GENDER { get; set; }
        public int C_AGE { get; set; }
        public DateTime? C_BIRTH_DATE { get; set; }
        public string C_CTF_TYPE { get; set; }
        public string C_PERSONAL_ID { get; set; }
        [Required]
        public string C_FIRSTNAME { get; set; }
        [Required]
        public string C_MIDDLENAME { get; set; }
        public string C_LASTNAME { get; set; }
        public string C_NATION { get; set; }
        public string C_NATIVEPLACE { get; set; }
        public string C_NATIONALITY { get; set; }
        public DateTime? C_FIRST_WORK_DATE { get; set; }
        public DateTime? C_HIREDATE { get; set; }
        public string C_MARITAL_STATUS { get; set; }
        public string C_POLITICAL_STATUS { get; set; }
        public string C_EDUCATION_LEVEL { get; set; }
        public string C_SPECIALTY { get; set; }
        public string C_DEGREE { get; set; }
        public string C_UNIVERSITY_COLLEGE { get; set; }
        public string C_OFFICE_TEL { get; set; }
        public string C_MOBILE_TEL { get; set; }
        public string C_BUSINESS_EMAIL { get; set; }
        public string C_PER_EMAIL { get; set; }
        public string C_ADDRESS { get; set; }
        public string C_WORK_PLACE { get; set; }
        public string C_COMPANY { get; set; }
        public string C_PHOTO { get; set; }
        public string C_REMARK { get; set; }
        public int C_ORDER { get; set; }
        public string C_OPERATE_TYPE { get; set; }
        public DateTime? C_UPDATE_TIME { get; set; }

        [Timestamp]
        public System.Byte[] C_TIMESTAMP { get; set; }

        public string TIMESTAMPS { get { return Untils.ConvertToTimeSpanString(C_TIMESTAMP); } }
    }
}