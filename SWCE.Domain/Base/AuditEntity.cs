using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    public abstract class AuditEntity
    {
        protected AuditEntity() {
            this.IsDeleted = false;
        }

        public bool? IsDeleted { get; set; } = false;
    }
}
