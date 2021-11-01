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
        public string Password { get; set; }

        public string Email { get; set; }
        public bool ISMigrated { get; set; }
        public int CustomerTypeID { get; set; } //enum
        public bool ISTrialBalanceOpted { get; set; }
    }
}
