using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.WebService.Providers;

namespace SuperHeroCatalogue.WebService.Controllers
{
    [RoutePrefix("api/SuperPower")]
    public class SuperPowerController : ApiController
    {
        private readonly IAuditAppService _auditAppService;
        private readonly ISuperPowerAppService _superPowerAppService;

        public SuperPowerController(){}

        public SuperPowerController(ISuperPowerAppService superPowerAppService, IAuditAppService auditAppService)
        {
            _auditAppService = auditAppService;
            _superPowerAppService = superPowerAppService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetPaginatedList/{page?}/{pageSize?}")]
        public PagedListProvider<SuperPowerModel> GetPaginatedList(int? page = null, int? pageSize = null)
        {
            var lst = new PagedListProvider<SuperPowerModel>(_superPowerAppService.GetAll(), page, pageSize);

            if (lst == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return lst;
        }

        [Authorize]
        [HttpGet]
        [Route("GetSigle/{id}")]
        public SuperPowerModel GetSigle(int id)
        {
            var superPower = _superPowerAppService.GetSigle(id);
            if (superPower == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return superPower;
        }

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage PostSuperPower([FromBody] SuperPowerModel superPower)
        {
            try
            {
                _superPowerAppService.Create(superPower);

                _auditAppService.Insert(new AuditEventViewModel()
                {
                    Action = "Create",
                    Datetime = DateTime.Now,
                    Entity = "SuperPower",
                    EntityId = superPower.Id,
                    UserName = ApplicationOAuthProvider.User.UserName
                });

                return Request.CreateResponse(HttpStatusCode.Created, superPower);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public void PutSuperPower([FromBody]SuperPowerModel superPower)
        {
            try
            {
                _superPowerAppService.Update(superPower);

                _auditAppService.Insert(new AuditEventViewModel()
                {
                    Action = "Update",
                    Datetime = DateTime.Now,
                    Entity = "SuperPower",
                    EntityId = superPower.Id,
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
        public void DeleteSuperPower(int id)
        {
            var superPower = _superPowerAppService.GetSigle(id);
            if (superPower == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _superPowerAppService.Delete(id);

            _auditAppService.Insert(new AuditEventViewModel()
            {
                Action = "Delete",
                Datetime = DateTime.Now,
                Entity = "SuperPower",
                EntityId = superPower.Id,
                UserName = ApplicationOAuthProvider.User.UserName
            });
        }
    }
}