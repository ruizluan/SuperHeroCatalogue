using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SuperHeroCatalogue.Infra.Data.Repositories.ReadOnly
{
    public class ContaReadOnlyRepository : BaseReadOnlyRepository, IContaReadOnlyRepository
    {
        public Conta GetById(int id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Conta
                            WHERE ContaId = @sid";

                var contas = conn.Query<Conta>(sql, new { sid = id }).First();

                return contas;
            }
        }
        public IEnumerable<Conta> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT * FROM Conta
                            ORDER BY Descricao ASC";

                var contas = conn.Query<Conta>(sql);

                return contas;
            }
        }
    }
}
