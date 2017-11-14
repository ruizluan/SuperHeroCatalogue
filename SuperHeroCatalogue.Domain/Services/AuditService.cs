using SuperHeroCatalogue.Domain.Entities;
using System;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Domain.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;

        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public void Insert(AuditEvent auditEvent)
        {
            _auditRepository.Insert(auditEvent);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
