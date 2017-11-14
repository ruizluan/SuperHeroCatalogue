using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories.ReadOnly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace SuperHeroCatalogue.Infra.Data.Repositories.ReadOnly
{
    public class UsuarioReadOnlyRepository : BaseReadOnlyRepository, IUsuarioReadOnlyRepository
    {
        public Usuario GetById(Guid id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT *
                            FROM Usuario
                            WHERE Excluido != 1 AND UsuarioId = @sid";

                var usuarios = conn.Query<Usuario>(sql, new { sid = id }).First();

                return usuarios;
            }
        }

        public Usuario GetByCodigo(string codigo)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT *
                            FROM Usuario
                            WHERE Excluido != 1 AND codigo = @scodigo";

                var usuarios = conn.Query<Usuario>(sql, new { scodigo = codigo});
                var usuario = usuarios.Count() > 0 ? usuarios.First() : new Usuario();

                return usuario;
            }
        }

        public Usuario GetByUserLogin(string login)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT *
                            FROM Usuario
                            WHERE Excluido != 1 AND lower(Login) = @slogin";

                var usuarios = conn.Query<Usuario>(sql, new { slogin = login });
                var usuario = usuarios.Count() > 0 ? usuarios.First() : new Usuario();

                return usuario;
            }
        }

        public IEnumerable<Usuario> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"SELECT *
                            FROM Usuario
                            WHERE Excluido != 1
                            ORDER BY Nome ASC";

                var usuarios = conn.Query<Usuario>(sql);
                
                return usuarios;
            }
        }

        public void Add(Usuario usuario)
        {
            using(IDbConnection conn = Connection)
            {
               conn.Open();
               var sql = @"
               INSERT INTO [dbo].[Usuario]
                          ([UsuarioId]
                          ,[Nome]
                          ,[DataCadastro]
                          ,[Ativo]
                          ,[PerfilId]
                          ,[Excluido]
                          ,[Email]
                          ,[Codigo]
                          ,[Login])
                    VALUES
                          (@sUsuarioId
                          ,@sNome
                          ,@sDataCadastro
                          ,@sAtivo
                          ,@sPerfilId
                          ,@sExcluido
                          ,@sEmail
                          ,@sCodigo
                          ,@sLogin)";

                conn.Query<Usuario>(sql, new { 
                    sUsuarioId = usuario.UsuarioId,
                    sNome = usuario.Nome,
                    sDataCadastro = DateTime.Now,
                    sAtivo = usuario.Ativo, 
                    sPerfilId = usuario.PerfilId,
                    sExcluido = usuario.Excluido,
                    sEmail = usuario.Email,
                    sCodigo = usuario.Codigo,
                    sLogin = usuario.Login
                });
               

            }
        }

        public void Update(Usuario usuario)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var sql = @"
                UPDATE [dbo].[Usuario]
                SET        
                     [Nome] = @sNome
                    ,[DataAlteracao] = @sDataAlteracao
                    ,[Ativo] = @sAtivo
                    ,[PerfilId] = @sPerfilId
                    ,[Excluido] = @sExcluido
                    ,[Email] = @sEmail
                    ,[Codigo] = @sCodigo
                    ,[Login] = @sLogin

                WHERE [UsuarioId] = @sUsuarioId";

                conn.Query<Usuario>(sql, new
                {
                    sUsuarioId = usuario.UsuarioId,
                    sNome = usuario.Nome,
                    sDataAlteracao = DateTime.Now,
                    sAtivo = usuario.Ativo,
                    sPerfilId = usuario.PerfilId,
                    sExcluido = usuario.Excluido,
                    sEmail = usuario.Email,
                    sCodigo = usuario.Codigo,
                    sLogin = usuario.Login
                });
            }
        }
    }
}
