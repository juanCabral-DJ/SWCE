 

namespace SWCE.Domain.Base
{
    public abstract class AuditEntity
    {
  
        public bool IsDeleted { get; set; } = false;
    }
}
