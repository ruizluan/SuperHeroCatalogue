using System.Collections.Generic;
using SuperHeroCatalogue.Domain.Entities;
using System.Linq;
using Dapper;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User GetSigle(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"SELECT Id, UserName, PasswordHash, Salt, IdRole FROM [dbo].[User] WHERE Id = @sId";
                var user = conn.Query<User>(query, new { sId = id }).First();

                return user;
            }
        }

        public IList<User> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"SELECT Id, UserName, PasswordHash, Salt, IdRole FROM [dbo].[User]";
                var lst = conn.Query<User>(query);

                return lst.ToList();
            }
        }

        public void Create(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                string query;

                if (user.Id == 0)
                {
                    query = @"INSERT INTO [dbo].[User]
                                ([UserName]
                                ,[PasswordHash]
                                ,[Salt]
                                ,[IdRole])
                            VALUES
                                (@sUserName
                                ,@sPasswordHash
                                ,@sSalt
                                ,@sIdRole)";
                }
                else
                {
                    query = @"UPDATE [dbo].[User] SET 
                                [UserName] = @sUserName
                                ,[PasswordHash] = @sPasswordHash
                                ,[Salt] = @sSalt
                                ,[IdRole] = @sIdRole
                             WHERE Id = @sId";
                }

                conn.Query<User>(query, new
                {
                    sId = user.Id,
                    sUserName = user.UserName,
                    sPasswordHash = user.PasswordHash,
                    sSalt = user.Salt,
                    sIdRole = user.IdRole
                });
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                const string query = @"DELETE [dbo].[User] WHERE Id = @sId";
                conn.Query<User>(query, new { sId = id });
            }
        }
    }
}