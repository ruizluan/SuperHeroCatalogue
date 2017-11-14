using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.WebService.Providers;

namespace SuperHeroCatalogue.WebService.Controllers
{
    [RoutePrefix("api/SuperHero")]
    public class SuperHeroController : ApiController
    {
        private readonly IAuditAppService _auditAppService;
        private readonly ISuperHeroAppService _superHeroAppService;

        public SuperHeroController(){}

        public SuperHeroController(ISuperHeroAppService superHeroAppService, IAuditAppService auditAppService)
        {
            _auditAppService = auditAppService;
            _superHeroAppService = superHeroAppService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetPaginatedList/{page?}/{pageSize?}")]
        public PagedListProvider<SuperHeroModel> GetPaginatedList(int? page = null, int? pageSize = null)
        {
            var lst = new PagedListProvider<SuperHeroModel>(_superHeroAppService.GetAll(), page, pageSize);

            if (lst == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return lst;
        }

        [Authorize]
        [HttpGet]
        [Route("GetSigle/{id}")]
        public SuperHeroModel GetSigle(int id)
        {
            var superHero = _superHeroAppService.GetSigle(id);
            if (superHero == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return superHero;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage PostSuperHero([FromBody] SuperHeroModel superHero)
        {
            try
            {
                _superHeroAppService.Create(superHero);

                _auditAppService.Insert(new AuditEventViewModel()
                {
                    Action = "Create",
                    Datetime = DateTime.Now,
                    Entity = "SuperPower",
                    EntityId = superHero.Id,
                    UserName = ApplicationOAuthProvider.User.UserName
                });

                return Request.CreateResponse(HttpStatusCode.Created, superHero);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public void PutSuperHero([FromBody]SuperHeroModel superHero)
        {
            try
            {
                _superHeroAppService.Update(superHero);

                _auditAppService.Insert(new AuditEventViewModel()
                {
                    Action = "Update",
                    Datetime = DateTime.Now,
                    Entity = "SuperPower",
                    EntityId = superHero.Id,
                    UserName = ApplicationOAuthProvider.User.UserName
                });
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public void DeleteSuperHero(int id)
        {
            var superHero = _superHeroAppService.GetSigle(id);
            if (superHero == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _superHeroAppService.Delete(id);
            _auditAppService.Insert(new AuditEventViewModel()
            {
                Action = "Delete",
                Datetime = DateTime.Now,
                Entity = "SuperPower",
                EntityId = superHero.Id,
                UserName = ApplicationOAuthProvider.User.UserName
            });
        }
    }
}
