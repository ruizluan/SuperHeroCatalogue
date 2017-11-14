using SuperHeroCatalogue.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class SuperHeroRepository : BaseRepository, ISuperHeroRepository
    {
        public SuperHero GetSigle(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();

                //const string query = @" SELECT [Id], [Name], [Alias], [IdProtectionArea]  FROM [SuperHero] WHERE Id = @sId";
                //var superHero = conn.Query<SuperHero>(query, new { sId = id }).First();
                //const string queryArea = @" SELECT [Id], [Name], [Lat], [Long], [Radius]  FROM [ProtectionArea] WHERE Id = @sId";
                //var protectionArea = conn.Query<ProtectionArea>(queryArea, new { sId = lookup.Values.FirstOrDefault().IdProtectionArea }).First();
                //superHero.ProtectionArea = protectionArea;
                //return superHero;

                var lookup = new Dictionary<int, SuperHero>();

                conn.Query<SuperHero, SuperPower, SuperHero>(
                    @"SELECT sh.*, sp.* FROM SuperHero sh INNER JOIN SuperPower sp ON sh.Id = sp.IdSuperHero", (sh, sp) =>
                    {
                        SuperHero superHero;
                        if (!lookup.TryGetValue(sh.Id, out superHero))
                        {
                            lookup.Add(sh.Id, superHero = sh);
                        }
                        if (superHero.SuperPowers == null)
                            superHero.SuperPowers = new List<SuperPower>();
                        superHero.SuperPowers.Add(sp);
                        return superHero;
                    }
                );

                var superH = lookup.Values.FirstOrDefault();

                const string queryArea = @" SELECT [Id], [Name], [Lat], [Long], [Radius]  FROM [ProtectionArea] WHERE Id = @sId";

                if (superH == null) return null;

                var protectionArea = conn.Query<ProtectionArea>(queryArea, new { sId = superH.IdProtectionArea }).First();

                superH.ProtectionArea = protectionArea;

                return superH;
            }
        }

        public IList<SuperHero> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                //const string query = @"SELECT Id, Name, Description FROM [SuperHero]";
                //var lst = conn.Query<SuperHero>(query);
                //return lst;

                var lookup = new Dictionary<int, SuperHero>();

                conn.Query<SuperHero, SuperPower, SuperHero>(@"
                    SELECT sh.*, sp.*
                    FROM SuperHero sh
                    LEFT JOIN SuperPower sp ON sh.Id = sp.IdSuperHero", (sh, sp) =>
                    {
                        SuperHero superHero;
                        if (!lookup.TryGetValue(sh.Id, out superHero))
                        {
                            lookup.Add(sh.Id, superHero = sh);
                        }
                        if (superHero.SuperPowers == null)
                            superHero.SuperPowers = new List<SuperPower>();
                        superHero.SuperPowers.Add(sp);
                        return superHero;
                    });

                return lookup.Values.ToList();
            }
        }

        public void Create(SuperHero superHero)
        {
            using (var conn = Connection)
            {
                conn.Open();

                const string queryInsert = @"INSERT INTO [dbo].[SuperHero] ([Name],[Alias],[IdProtectionArea]) VALUES (@sName, @sAlias, @sIdProtectionArea)";
                const string queryUpdate = @"UPDATE [dbo].[SuperHero] SET [Name] = @sName, [Alias] = @sAlias, [IdProtectionArea] = @sIdProtectionArea WHERE Id = @sId";

                conn.Query<SuperHero>(superHero.Id == 0 ? queryInsert : queryUpdate, new
                {
                    sId = superHero.Id,
                    sName = superHero.Name,
                    sAlias = superHero.Alias,
                    sIdProtectionArea = superHero.IdProtectionArea
                });
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"DELETE [SuperHero] WHERE Id = @sId
                                       DELETE [SuperPower] WHERE IdSuperHero = @sId
                                       DELETE [ProtectionArea] WHERE Id = (SELECT IdProtectionArea FROM [SuperHero] WHERE Id = @sId)";
                conn.QueryMultiple(query, new { sId = id });
            }
        }

        public void CreateProtectionArea(ProtectionArea protectionArea)
        {
            using (var conn = Connection)
            {
                conn.Open();

                const string queryInsert =
                    @"INSERT INTO [dbo].[ProtectionArea] ([Name],[Lat],[Long],[Radius]) VALUES (@sName, @sLat, @sLong, @sRadius)";
                const string queryUpdate =
                    @"UPDATE [dbo].[ProtectionArea] SET [Name] = @sName, [Lat] = @sLat, [Long] = @sLong, [Radius] = @sRadius WHERE Id = @sId";

                conn.Query<SuperHero>(protectionArea.Id == 0 ? queryInsert : queryUpdate, new
                {
                    sId = protectionArea.Id,
                    sName = protectionArea.Name,
                    sLat = protectionArea.Lat,
                    sLong = protectionArea.Long,
                    sRadius = protectionArea.Radius
                });
            }
        }

        public ProtectionArea GetLastProtectionArea()
        {
            using (var conn = Connection)
            {
                conn.Open();

                const string query = @"SELECT * FROM dbo.protectionArea where id= (SELECT MAX(id)FROM dbo.protectionArea)";
                var protectionArea = conn.Query<ProtectionArea>(query).First();

                return protectionArea;
            }
        }
    }
}
