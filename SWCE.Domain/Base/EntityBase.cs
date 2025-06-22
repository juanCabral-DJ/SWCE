using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    public abstract class EntityBase<Ttype> : AuditEntity
    {
        public abstract Ttype id { get; set; }
    }
}
