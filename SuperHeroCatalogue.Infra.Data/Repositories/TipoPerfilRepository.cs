﻿using SuperHeroCatalogue.Domain.Entities;
using SuperHeroCatalogue.Domain.Interfaces.Repositories;
using SuperHeroCatalogue.Infra.Data.Context;

namespace SuperHeroCatalogue.Infra.Data.Repositories
{
    public class TipoPerfilRepository : BaseRepository<TipoPerfil, CatalogoRubricasNestle>, ITipoPerfilRepository
    {
        
    }
}
