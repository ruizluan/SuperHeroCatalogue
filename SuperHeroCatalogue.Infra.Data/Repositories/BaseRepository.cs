using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class BaseRepository
    {
        public IDbConnection Connection => new SqlConnection(ConfigurationManager.ConnectionStrings["SuperHeroCatalogue"].ConnectionString);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}