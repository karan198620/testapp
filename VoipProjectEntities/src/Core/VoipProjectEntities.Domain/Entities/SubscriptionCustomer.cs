using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Voip2.Domain.Common;

namespace Voip2.Domain.Entities
{
    public class SubscriptionCustomer : CommonField
    {
        public Guid SubscriptionCustomerID { get; set; }

        [Display(Name = "Customer")]
        public Guid CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customers { get; set; }

        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public int SubscriptionType { get; set; }
        public bool ISActive { get; set; }

    }
}
