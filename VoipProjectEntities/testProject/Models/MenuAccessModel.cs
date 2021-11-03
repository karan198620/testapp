using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testProject.Models
{
    public class MenuAccessModel
    {
        public Guid MenuAccessId { get; set; }
        public bool IsAccess { get; set; }
        public int MenuLink { get; set; } //enum
        public Guid CustomerID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
