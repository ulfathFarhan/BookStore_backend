using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStoreDataAccess;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;

namespace BookStore.Controllers
{
    [RoutePrefix("api/Boooks")]
    public class BoooksController : ApiController
    {
        [Route("")]
        [HttpGet]
        public List<Book> AllBooks()
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                return entities.Books.ToList();
            }
        }
        [HttpGet]
        [Route("{id:int}", Name = "BookById")]

        public HttpResponseMessage BookById(int id)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.Books.FirstOrDefault(b=> b.BookId == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Book with Id = " + id.ToString() + " is not found");
                }
            }
        }


        [Route("name/{name}")]
        [HttpGet]
        public HttpResponseMessage BookByName(string name)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.Books.Where(c => c.Title.ToLower() == name.ToLower()).FirstOrDefault<Book>();
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with Name = " + name + " is not found");
                }
            }
        }

        [Route("isbn/{isbn}")]
        [HttpGet]
        public HttpResponseMessage BookByISBN(string isbn)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.Books.Where(c => c.ISBN == isbn).FirstOrDefault<Book>();
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User with ISBN = " + isbn + " is not found");
                }
            }
        }
        [Route("category/{category}")]
        [HttpGet]
        public HttpResponseMessage BookByCategory(string category)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                var entity = entities.Books.Where(c => c.Category.CategoryName.ToLower()== category.ToLower()).ToList();
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User under this Category  = " + category + " is not found");
                }
            }
        }
    }

}
