using AutoMapper;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Services;

namespace SuperHeroCatalogue.Application.Services
{
    public class AuditAppService : IAuditAppService
    {
        private readonly IAuditService _auditService;

        public AuditAppService(IAuditService auditService)
        {
            _auditService = auditService;
        }

        public void Insert(AuditEventViewModel auditEvent)
        {
            _auditService.Insert(Mapper.Map<AuditEventViewModel, AuditEvent>(auditEvent));
        }
        public void Dispose()
        {
            _auditService.Dispose();
        }
    }
}