using BookStoreDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BookStore.Controllers
{
        [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        [Route("")]
        [HttpGet]
        public List<Category> AllCategory()
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                return entities.Categories.ToList();
            }
        }
    }
}
