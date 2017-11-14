using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SuperHeroCatalogue.Infra.Data.Repositories.ReadOnly
{
    public class PerfilReadOnlyRepository : BaseReadOnlyRepository, IPerfilReadOnlyRepository
    {
        public Perfil GetById(Guid id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Perfil
                            WHERE PerfilId = @sid";

                var perfils = conn.Query<Perfil>(sql, new { sid = id }).First();

                return perfils;
            }
        }
        public IEnumerable<Perfil> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Perfil
                            ORDER BY Descricao ASC";

                var perfils = conn.Query<Perfil>(sql);

                return perfils;
            }
        }
    }
}
