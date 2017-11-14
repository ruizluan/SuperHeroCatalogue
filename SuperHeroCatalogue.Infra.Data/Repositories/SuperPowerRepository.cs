using SuperHeroCatalogue.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class SuperPowerRepository : BaseRepository, ISuperPowerRepository
    {
        public SuperPower GetSigle(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"SELECT [Id], [Name], [Description], [IdSuperHero] FROM [dbo].[SuperPower] WHERE Id = @sId";
                var superPower = conn.Query<SuperPower>(query, new { sId = id }).First();

                return superPower;
            }
        }

        public IList<SuperPower> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"SELECT [Id], [Name], [Description], [IdSuperHero]  FROM [dbo].[SuperPower]";
                var lst = conn.Query<SuperPower>(query);

                return lst.ToList();
            }
        }

        public void Create(SuperPower superPower)
        {
            using (var conn = Connection)
            {
                conn.Open();
                string query;

                if (superPower.Id == 0)
                {
                    query = @"INSERT INTO [dbo].[SuperPower]
                                ([Name]
                                ,[Description]
                                ,[IdSuperHero])
                            VALUES
                                (@sName
                                ,@sDescription
                                ,@sIdSuperHero)";
                }
                else
                {
                    query = @"UPDATE [dbo].[SuperPower] SET 
                                 [Name] = @sName
                                ,[Description] = @sDescription
                             WHERE Id = @sId";
                }

                conn.Query<SuperPower>(query, new
                {
                    sId = superPower.Id,
                    sName = superPower.Name,
                    sDescription = superPower.Description,
                    sIdSuperHero = superPower.IdSuperHero

                });
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"DELETE [dbo].[SuperPower] WHERE Id = @sId";
                conn.Query<SuperPower>(query, new { sId = id });
            }
        }
    }
}
