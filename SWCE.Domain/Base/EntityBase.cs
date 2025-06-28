 

namespace SWCE.Domain.Base
{
    public abstract class EntityBase<Ttype> : AuditEntity
    {
        public abstract Ttype id { get; set; }
    }
}
