using Antlr.Runtime.Misc;
using BookStore.Controllers;
using BookStoreDataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Xml;

namespace BookStore
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray =decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                if (UserSecurity.Login(username, password))
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myDB"].ConnectionString);
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "select * from Users where username='" +username+"' and password ='"+password+"'";
                    command.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    User obj = new User();
                    while (reader.Read())
                    {
                            obj.UserId  =Convert.ToInt32(reader["UserId"]);
                          obj.UserName  = reader["UserName"].ToString();
                            obj.email  = reader["email"].ToString();
                        obj.password  = reader["password"].ToString();
                        obj.address = reader["address"].ToString();
                        obj.phone_no  = reader["phone_no"].ToString();
                        obj.active =Convert.ToInt32( reader["active"]);
                        obj.isAdmin =Convert.ToInt32( reader["isAdmin"]);
                        
                       OrderedItemsController.currentUser.Add(obj);
                    }
                    conn.Close();
                    Debug.Print((OrderedItemsController.currentUser[0].UserId).ToString());
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }


            }
        }
    }
}