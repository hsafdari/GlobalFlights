using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalFlights.Common.Entity
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid UserId { get; set; }
    }
}
