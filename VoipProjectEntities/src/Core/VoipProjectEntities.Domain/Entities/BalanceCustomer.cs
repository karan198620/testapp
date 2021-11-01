using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Voip2.Domain.Entities;

namespace VoipMainProject.Domain.Entities
{
    public class BalanceCustomer
    {
        [Key]
        public Guid BalanceCustomerID { get; set; }

        public double BalanceAmount { get; set; }

        public int TranscationType { get; set; }


        [Display(Name = "Customer")]
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customers { get; set; }
    }
}
