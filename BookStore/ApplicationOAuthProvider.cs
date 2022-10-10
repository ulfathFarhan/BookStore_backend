//using BookStore.Models;
//using Microsoft.AspNet.Identity.EntityFramework;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Security.OAuth;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Web;
//using BookStoreDataAccess;

//namespace BookStore
//{
//    public class ApplicationOAuthProvider :OAuthAuthorizationServerProvider
//    {

//        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
//        {
//            context.Validated();
//        }

//        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
//        {
//            var userStore = new UserStore<User>(new bookStoreDBEntities());
//            var manager = new UserManager<User>(userStore);
//            var user = await manager.FindAsync(context.UserName, context.Password);
//            if (user != null)
//            {
//                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
//                identity.AddClaim(new Claim("Username", user.UserName));
//                identity.AddClaim(new Claim("Email", user.Email));
//                identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
//                context.Validated(identity);
//            }
//            else
//                return;
//        }
//    }
//}