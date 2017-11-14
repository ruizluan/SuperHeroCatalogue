using SuperHeroCatalogue.Application.Models;

namespace SuperHeroCatalogue.Application.Interfaces
{
    public interface IAuditAppService
    {
        void Insert(AuditEventViewModel auditEvent);
    }
}