using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseParser.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required]
        [DisplayName("Payroll Number")]
        public string PayrollNumber { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [DisplayName("Forename")]
        public string ForeNames { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [DisplayName("Surname")]
        public string SurName { get; set; }

        [Column(TypeName = "varchar(15)")]
        [DisplayName("Date of birth")]
        public string DateOfBirth { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Telephone { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Mobile { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Adress { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Adress2 { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string PostCode { get; set; }

        [Column(TypeName = "varchar(50)")]
        [DisplayName("Home email")]
        public string EmailHome { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [DisplayName("Start date")]
        public string StartDate { get; set; }
    }
}