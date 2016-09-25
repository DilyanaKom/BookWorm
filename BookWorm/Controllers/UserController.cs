using BookWorm.Data;
using BookWorm.Headers;
using BookWorm.Infrastructure;
using BookWorm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ValueProviders;

namespace BookWorm.Controllers
{


    //string encr = keys.EncryptString(message);

    //// later
    //string actual = keys.DecryptString(encr);
    public class UserController : BaseApiController
    {

        [HttpPost, ActionName("logout")]
        public HttpResponseMessage LogoutUser([ValueProvider(typeof(HeaderValueProviderFactory<string>))] string token)
        {
            if (ValidateToken(token))
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var tokenDb = db.Tokens.FirstOrDefault(s => s.Value == token);
                    tokenDb.IsValid = false;
                    db.SaveChanges();

                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = true }) };
                }
            }
            else
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "Authentication failed!" }) };
            }
        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage GetAllTokens()
        {
            try
            {

                using (ApplicationDbContext db = new ApplicationDbContext())
                {


                    var t = db.Tokens.Select(s=>s).ToList();

                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, data = t }) };


                }



            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = ex.Message }) };

            }
        }



        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Login(LoginUserViewModel user)
        {
            try
            {

                using (ApplicationDbContext db = new ApplicationDbContext())
                {

                    var dbUser = db.Users.FirstOrDefault(s => s.Username == user.Username);

                    if (dbUser != null)
                    {


                        Encryptor keys = new Encryptor();

                        var dbPass = keys.DecryptString(dbUser.Password);

                        if (dbPass == user.Password)
                        {


                            Token token = new Token();
                            token.UserId = dbUser.Id;
                            token.Expire = DateTime.Now.AddDays(10);
                            token.IsValid = true;
                            token.Value = Guid.NewGuid().ToString();

                            db.Tokens.Add(token);
                            db.SaveChanges();

                            return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, data = token.Value }) };
                        }
                        else
                        {
                            return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "Wrong password!" }) };
                        }


                    }
                    else
                    {
                        return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "No user with this username!" }) };
                    }





                }



            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = ex.Message }) };

            }
        }

        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage Register(RegisterUserViewModel user)
        {
            try
            {
                if (user.Password != user.ConfirmPassword)
                {
                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "Passwords do not match!" }) };
                }

                User newUser = new User();
                newUser.Username = user.Username;

                Encryptor keys = new Encryptor();
                newUser.Password = keys.EncryptToString(user.Password);


                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var dupUser = db.Users.FirstOrDefault(s => s.Username == newUser.Username);
                    if (dupUser != null)
                    {
                        return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "This username is already taken!" }) };
                    }

                    db.Users.Add(newUser);
                    db.SaveChanges();


                    Token token = new Token();
                    token.UserId = newUser.Id;
                    token.Expire = DateTime.Now.AddDays(10);
                    token.IsValid = true;
                    token.Value = Guid.NewGuid().ToString();

                    db.Tokens.Add(token);
                    db.SaveChanges();



                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, data = token.Value }) };
                }



            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = ex.Message }) };

            }
        }
    }
}
