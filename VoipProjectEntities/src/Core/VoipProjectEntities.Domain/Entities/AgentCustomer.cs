using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Voip2.Domain.Common;
using Voip2.Domain.Entities;

namespace VoipMainProject.Domain.Entities
{
    public class AgentCustomer : CommonField
    {
        [Key]
        public Guid AgentCustomerID { get; set; }

        public string AgentName { get; set; }

        public string Password { get; set; }

        public bool ISMigratedAt { get; set; }


        [Display(Name = "Customer")]
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }

    }
}
