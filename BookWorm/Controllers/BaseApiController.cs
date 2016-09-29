using BookWorm.Data;
using BookWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookWorm.Controllers
{
    public class BaseApiController : ApiController
    {

        protected User GetUserByToken(string token)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            { 
                var tokenDb = db.Tokens.FirstOrDefault(s => s.Value == token);
                return tokenDb.User;
            }
        }

        protected bool ValidateToken(string token)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var tokenDb = db.Tokens.FirstOrDefault(s => s.Value == token);
                if (tokenDb==null)
                {
                    return false;
                }
                else
                {
                    if (!tokenDb.IsValid)
                    {
                        return false;
                    }
                    else if(tokenDb.Expire.Date < DateTime.Now.Date)
                    {
                        tokenDb.IsValid = false;
                        db.SaveChanges();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }
}
