using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Employee.Models
{
    public class EmployeeSearchViewModel
    {
     
       
        [DisplayName("Last Name")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters only")]
        public string EmployeeLastName { get; set; }

        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string EmployeePhone { get; set; }

    }
}
