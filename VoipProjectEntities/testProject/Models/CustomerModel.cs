using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testProject.Models
{
    public class CustomerModel
    {
        public Guid CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirmed Password did not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool ISMigrated { get; set; }
        public int CustomerTypeID { get; set; } //enum
        public bool ISTrialBalanceOpted { get; set; }
    }
}
