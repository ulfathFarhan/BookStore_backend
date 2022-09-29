using BookStoreDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers
{
    [RoutePrefix("api/OrderItems")]
    public class OrderItemController : ApiController
    {
        [HttpGet]
        public List<OrderItem> AllOrderItems()
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                return entities
                    .OrderItems
                    .ToList();
            }
        }
        [HttpGet]
        [Route("{id:int}", Name = "OrderItemById")]

        public HttpResponseMessage OrderItemById(int id)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.OrderItems.FirstOrDefault(o => o.OrderItemId == id);
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
    }
}
