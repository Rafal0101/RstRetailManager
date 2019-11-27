using Microsoft.AspNet.Identity;
using RSTDataManager.Library.DataAccess;
using RSTDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RSTDataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public string Get(int id)
        {
            return "value";
        }

        // GET: User/Details/5
        public List<UserModel> GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId);
        }

     
    }
}
