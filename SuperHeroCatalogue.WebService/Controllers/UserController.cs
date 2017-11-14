using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using SuperHeroCatalogue.Application.Interfaces;
using SuperHeroCatalogue.Application.Models;
using SuperHeroCatalogue.WebService.Providers;

namespace SuperHeroCatalogue.WebService.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IAuditAppService _auditAppService;
        private readonly IUserAppService _userAppService;

        public UserController() { }

        public UserController(IUserAppService userAppService, IAuditAppService auditAppService)
        {
            _auditAppService = auditAppService;
            _userAppService = userAppService;
        }

        [Authorize]
        [HttpGet]
        [Route("GetPaginatedList/{page?}/{pageSize?}")]
        public PagedListProvider<UserModel> GetPaginatedList(int? page = null, int? pageSize = null)
        {

            var lst = new PagedListProvider<UserModel>(_userAppService.GetAll().AsQueryable(), page, pageSize);

            if (lst == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return lst;
        }

        //[Authorize]
        //[HttpGet]
        //[Route("GetSigle/{id}")]
        //public UserModel GetSigle(int id)
        //{
        //    var user = _userAppService.GetSigle(id);
        //    if (user == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    return user;
        //}

        [Authorize]
        [HttpPost]
        [Route("Create")]
        public HttpResponseMessage PostUser([FromBody] UserModel user)
        {
            try
            {
                _userAppService.Create(user);

                _auditAppService.Insert(new AuditEventViewModel()
                {
                    Action = "Create",
                    Datetime = DateTime.Now,
                    Entity = "User",
                    EntityId = user.Id,
                    UserName = ApplicationOAuthProvider.User.UserName
                });

                return Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("Update")]
        public void PutUser([FromBody]UserModel user)
        {
            try
            {
                _userAppService.Update(user);

                _auditAppService.Insert(new AuditEventViewModel()
                {
                    Action = "Update",
                    Datetime = DateTime.Now,
                    Entity = "User",
                    EntityId = user.Id,
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
        public void DeleteUser(int id)
        {
            var user = _userAppService.GetSigle(id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _userAppService.Delete(id);

            _auditAppService.Insert(new AuditEventViewModel()
            {
                Action = "Delete",
                Datetime = DateTime.Now,
                Entity = "User",
                EntityId = id,
                UserName = ApplicationOAuthProvider.User.UserName
            });
        }
    }
}