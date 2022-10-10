using BookStoreDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers
{
    [RoutePrefix("api/OrderedItems")]
    public class OrderedItemsController : ApiController
    {
        public static List<User> currentUser = new List<User>();
        [HttpGet]
        public List<OrderedItem> AllOrderItems()
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                return entities
                    .OrderedItems
                    .ToList();
            }
        }
        [Authorize]
        [HttpGet]
        [Route("{userid:int}", Name = "OrderedItemByUserId")]

        public HttpResponseMessage OrderedItemById(int userid)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.OrderedItems.Where(o=> o.UserId == userid).ToList();
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "OrderItems with UserId = " + userid.ToString() + " is not found");
                }
            }
        }
        [BasicAuthentication]
        [HttpPost]
        public HttpResponseMessage Post([FromBody] List<OrderedItem> listOfItems)
        {
            try
            {
                using (bookStoreDBEntities entities = new bookStoreDBEntities())
                {
                    foreach (var el in listOfItems)
                    {
                        OrderedItem o = new OrderedItem();
                        o.BookId = el.BookId;
                        o.UserId = currentUser[0].UserId;
                        o.quantity = el.quantity;
                        o.BookPrice = el.BookPrice;
                        o.TotalPrice = el.BookPrice * o.quantity;
                             entities.OrderedItems.Add(o);
                    }
                   
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, listOfItems);
       
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
