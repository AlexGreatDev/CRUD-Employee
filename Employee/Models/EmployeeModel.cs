using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters only")]
        public string EmployeeLastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters only")]

        public string EmployeeFirstName { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        [DisplayName("Employee Phone")]
        [Required(ErrorMessage = "This Field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]

        public string EmployeePhone { get; set; }

        [Column(TypeName = "nvarchar(11)")]
        [DisplayName("EmployeeZip")]
        [Required(ErrorMessage = "This Field is required.")]
        [MaxLength(11, ErrorMessage = "Maximum 11 characters only")]
        public string EmployeeZip { get; set; }


        [DisplayName("Create Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        [DisplayName("Hire Date")]
        public DateTime HireDate { get; set; }
    }
}
