using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SuperHeroCatalogue.Infra.Data.Repositories.ReadOnly
{
    public class TipoPerfilReadOnlyRepository : BaseReadOnlyRepository, ITipoPerfilReadOnlyRepository
    {
        public TipoPerfil GetById(Guid id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM TipoPerfil
                            WHERE TipoPerfilId = @sid";

                var tiposPerfis = conn.Query<TipoPerfil>(sql, new { sid = id }).First();

                return tiposPerfis;
            }
        }
        public IEnumerable<TipoPerfil> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM TipoPerfil
                            ORDER BY Descricao ASC";

                var tiposPerfis = conn.Query<TipoPerfil>(sql);

                return tiposPerfis;
            }
        }
    }
}
