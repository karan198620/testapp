using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
//using Voip.Domain.Common;
using Voip2.Domain.Common;
using Voip2.Domain.Entities;

namespace Voip.Domain.Entities
{
    public class MenuAccess: CommonField
    {
        [Key]
        public Guid MenuAccessId { get; set; }
        public bool IsAccess { get; set; }
        public int MenuLink { get; set; } //enum
        [Display(Name = "Customer")]
        public Guid? CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer Customers { get; set; }
    }
}
