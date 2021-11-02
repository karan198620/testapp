using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testProject.Models
{
    public class CustomerViewModel
    {
        public string CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool ISMigrated { get; set; }

        [Required]
        public CustomerType CustomerTypeID { get; set; } //enum
        public bool ISTrialBalanceOpted { get; set; }
    }
    public enum CustomerType
    {
        User = 0,
        Agents = 1,
        Demo = 2
    }
}
