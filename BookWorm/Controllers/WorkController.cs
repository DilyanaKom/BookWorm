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
using System.Web.Http.ValueProviders;

namespace BookWorm.Controllers
{
    public class WorkController : BaseApiController
    {
        //  [HttpPost, ActionName("create")]
        //public HttpResponseMessage CreateEvent([FromBody] EventViewModel eventModel,
        //    [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionKey)
        //{
        //    return this.PerformOperationAndHandleExceptions(() =>
        //        {
        //            var context = this.ContextFactory.Create();
        //            var user = this.LoginUser(sessionKey, context);
        //            var category = context.Set<Category>().Find(Convert.ToInt32(eventModel.CategoryId));

        //            if (category == null)
        //            {
        //                throw new InvalidOperationException("Invalid Category");
        //            }

        //            var newEvent = new Event()
        //            {
        //                Name = eventModel.Name,
        //                Longitude = eventModel.Longitude,
        //                Latitude = eventModel.Latitude,
        //                User = user,
        //                StartTime = eventModel.StartDate,
        //                Description = eventModel.Description,
        //                Duration = eventModel.StartDate.AddHours(eventModel.Duration.Hour).AddMinutes(eventModel.Duration.Minute)
        //            };

        //            var contacts = this.AddOrUpdateContacts(eventModel.AccociatedContacts, context);

        //            foreach (var contact in contacts)
        //            {
        //                newEvent.AccociatedContacts.Add(contact);
        //            }

        //            category.Events.Add(newEvent);
        //            context.SaveChanges();

        //            return this.Request.CreateResponse(HttpStatusCode.Created);
        //        });
        //}


        //,[ValueProvider(typeof(HeaderValueProviderFactory<string>))] string sessionToken


        [HttpGet]
        public HttpResponseMessage GetAllWorks(int? genreId)
        {
            try
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var works = db.Works.ToList().Select(s => new SimpleWorkViewModel()
                    {
                        Id = s.Id,
                        Body = s.Body,
                        Title = s.Title,
                        Genre = s.Genre.ToString()
                    }).ToList();


                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, data = works }) };
                }



            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = ex.Message }) };

            }
        }
    }
}
