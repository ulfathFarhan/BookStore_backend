using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;
using BookStoreDataAccess;
namespace BookStore.Controllers
{
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        
        [HttpGet]
        public List<User> Allusers()
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                return entities
                    .Users
                    .ToList();
            }
        }
        [HttpGet]
        [Route("{id:int}", Name = "UserById")]

        public HttpResponseMessage UserById(int id)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.Users.FirstOrDefault(u => u.UserId == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id = " + id.ToString() + " is not found");
                }
            }
        }

        [HttpPost]
        [Route("{register}", Name = "register")]

        public HttpResponseMessage Post([FromBody] User user)
        {
            try
            {
                using (bookStoreDBEntities entities = new bookStoreDBEntities())
                {
                    entities.Users.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Url.Link("UserById", new { id = user.UserId }));
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (bookStoreDBEntities entities = new bookStoreDBEntities())
                {
                    var entity = entities.Users.FirstOrDefault(u => u.UserId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id = " + id.ToString() + " is not found");

                    }
                    else
                    {
                        entities.Users.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, [FromBody] User user)
        {
            try
            {
                using (bookStoreDBEntities entities = new bookStoreDBEntities())
                {
                    var entity = entities.Users.FirstOrDefault(u => u.UserId == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Id = " + id.ToString() + " is not found");

                    }
                    else
                    {
                        entity.active = user.active;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);

                    }


                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
