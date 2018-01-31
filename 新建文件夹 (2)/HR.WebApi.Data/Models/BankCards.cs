using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HR.WebApi.Data.Models
{
    [Serializable]
    [Table("BankCards")]
    public class BankCards
    {
        [Key]
        public string Id { get; set; }

        public string EmployeeId { get; set; }

        public string BandName { get; set; }

        public string BankCard { get; set; }
    }
}
