using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Infra.Data.Context;
using System.Data.Entity;
using System.Collections.Generic;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario, CatalogoRubricasNestle>, IUsuarioRepository
    {
        public override void Remove(Usuario obj)
        {
            obj.Excluido = true;
            Update(obj);
        }
    }
}
