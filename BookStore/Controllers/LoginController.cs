using BookStore.Models;
using BookStoreDataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BookStore.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IJwtAuthManager jwtAuthManager;
        public LoginController()
        {

        }

        public LoginController(IJwtAuthManager jwtAuthManager)
        {
            this.jwtAuthManager = jwtAuthManager;
        }

        [AllowAnonymous]
        [HttpPost]
        //[Route("api/login")]
        public IActionResult Login([FromBody] User user)
        {
            var token = jwtAuthManager.Authenticate(user.UserName,user.password);
            if (token == null)
                return (IActionResult)Unauthorized();
            return (IActionResult)Ok(token);
        }
    }
}
