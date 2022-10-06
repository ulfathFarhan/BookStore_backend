using BookStoreDataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;

namespace BookStore.Controllers
{
    //public class AccountController : ApiController
    //{
    //    [Route("api/User/Register")]
    //    [HttpPost]
    //    public IdentityResult Register([FromBody] User user)
    //    {
    //        var userStore = new UserStore<User>(new ApplicationDbContext)

    //    }
    //}

    //[HttpPost]
    //public HttpResponseMessage Post([FromBody] User user)
    //{
    //    try
    //    {
    //        using (bookStoreDBEntities entities = new bookStoreDBEntities())
    //        {
    //            entities.Users.Add(user);
    //            entities.SaveChanges();

    //            var message = Request.CreateResponse(HttpStatusCode.Created, user);
    //            message.Headers.Location = new Uri(Url.Link("UserById", new { id = user.UserId }));
    //            return message;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
    //    }
    //}
}
