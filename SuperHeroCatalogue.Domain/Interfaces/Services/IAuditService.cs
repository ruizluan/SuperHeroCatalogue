using System;
using SuperHeroCatalogue.Domain.Entities;

namespace SuperHeroCatalogue.Domain.Interfaces.Services
{
    public interface IAuditService : IDisposable
    {
        void Insert(AuditEvent auditEvent);
    }
}