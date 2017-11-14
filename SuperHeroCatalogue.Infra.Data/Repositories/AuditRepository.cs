using SuperHeroCatalogue.Domain.Entities;
using Dapper;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class AuditRepository : BaseRepository, IAuditRepository
    {
        public void Insert(AuditEvent auditEvent)
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @" INSERT INTO [dbo].[AuditEvent]
                                             ([Id]
                                             ,[Entity]
                                             ,[EntityId]
                                             ,[Datetime]
                                             ,[UserName]
                                             ,[Action])
                                         VALUES
                                             (@sEntity
                                             ,@sEntityId
                                             ,@sDatetime
                                             ,@sUserName
                                             ,@sAction)";


                conn.Query<AuditEvent>(query, new
                {
                    sEntity     = auditEvent.Entity,
                    sEntityId   = auditEvent.EntityId,
                    sDatetime   = auditEvent.Datetime,
                    sUserName   = auditEvent.UserName,
                    sAction = auditEvent.Action
                });
            }
        }
    }
}