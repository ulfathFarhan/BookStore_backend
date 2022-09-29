using BookStoreDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BookStore
{
    public class UserSecurity
    {
        public static bool Login(string username,string password)
        {
            using (bookStoreDBEntities entities = new bookStoreDBEntities())
            {
                return entities.Users.Any(user => user.UserName.Equals(username,StringComparison.OrdinalIgnoreCase) && user.password == password);
            }
        }
    }
}