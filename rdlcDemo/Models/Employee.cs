using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rdlcDemo.Models
{
    public class Employee
    {   
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage ="This Field is Required")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Salary { get; set; }

    }
}