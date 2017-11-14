using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Repositories
{
    public interface IAuditRepository
    {
        void Insert(AuditEvent audit);
    }
}