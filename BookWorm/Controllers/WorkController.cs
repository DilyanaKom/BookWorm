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


        [HttpPost, ActionName("creatework")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage CreateWork([FromBody] WorkCreateViewModel work, [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string token)
        {
            if (ValidateToken(token))
            {
                var currentUser = GetUserByToken(token);

                using (ApplicationDbContext db = new ApplicationDbContext())
                {

                    Work workDb = new Work();
                    workDb.UserId = currentUser.Id;
                    workDb.CoverPhoto = work.CoverPhoto;
                    workDb.Body = work.Body;
                    workDb.Title = work.Title;
                    workDb.Genre = work.Genre;

                    for (int i = 0; i < work.Tags.Count; i++)
                    {
                        string value = work.Tags[i];
                        var dbTag = db.Tags.FirstOrDefault(s => s.Value == value.ToLower());

                        if (dbTag != null)
                        {
                            workDb.Tags.Add(dbTag);
                        }
                        else
                        {
                            Tag tag = new Tag();
                            tag.Value = value.ToLower();
                            workDb.Tags.Add(tag);

                        }
                    }

                    db.Works.Add(workDb);

                    db.SaveChanges();


                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, Data = workDb.Id }) };
                }
            }
            else
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "Authentication failed!" }) };
            }
        }


        [HttpGet, ActionName("workDetails")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage WorkDetails(int workId)
        {



            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var workDb = db.Works.FirstOrDefault(s => s.Id == workId);

                WorkDetailsViewModel work = new WorkDetailsViewModel();
                work.Id = workDb.Id;
                work.User = workDb.User.Username;
                work.CoverPhoto = workDb.CoverPhoto;
                work.Body = workDb.Body;
                work.Title = workDb.Title;
                work.Genre = workDb.Genre.ToString();

                for (int i = 0; i < workDb.Tags.Count; i++)
                {
                    work.Tags.Add(workDb.Tags.ElementAt(i).Value);
                }


                return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, Data = work }) };
            }

        }

        [HttpGet, ActionName("workLibrary")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage WorkLibrary(int offset, int count)
        {



            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var items = db.Works.OrderBy(s => s.Id).Skip(offset).Take(count).ToList();
                var result = new List<WorkDetailsViewModel>();

                for (int i = 0; i < items.Count; i++)
                {
                    WorkDetailsViewModel work = new WorkDetailsViewModel();
                    work.Id = items[i].Id;
                    work.User = items[i].User.Username;
                    work.CoverPhoto = items[i].CoverPhoto;
                    work.Body = items[i].Body;
                    work.Title = items[i].Title;
                    work.Genre = items[i].Genre.ToString();

                    for (int j = 0; j < items[i].Tags.Count; j++)
                    {
                        work.Tags.Add(items[i].Tags.ElementAt(j).Value);
                    }

                    result.Add(work);

                }


                return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, Data = result }) };
            }

        }

        [HttpGet, ActionName("myworkLibrary")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage MyWorkLibrary(int offset, int count, [ValueProvider(typeof(HeaderValueProviderFactory<string>))] string token)
        {

            if (ValidateToken(token))
            {
                var currentUser = GetUserByToken(token);
                using (ApplicationDbContext db = new ApplicationDbContext())
                {

                    var items = db.Works.Where(s=>s.UserId == currentUser.Id).OrderBy(s => s.Id).Skip(offset).Take(count).ToList();
                    var result = new List<WorkDetailsViewModel>();

                    for (int i = 0; i < items.Count; i++)
                    {
                        WorkDetailsViewModel work = new WorkDetailsViewModel();
                        work.Id = items[i].Id;
                        work.User = items[i].User.Username;
                        work.CoverPhoto = items[i].CoverPhoto;
                        work.Body = items[i].Body;
                        work.Title = items[i].Title;
                        work.Genre = items[i].Genre.ToString();

                        for (int j = 0; j < items[i].Tags.Count; j++)
                        {
                            work.Tags.Add(items[i].Tags.ElementAt(j).Value);
                        }

                        result.Add(work);

                    }


                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, Data = result }) };
                }
            }
            else
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = "Authentication failed!" }) };
            }
        }


        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage AddWorkNoAuthentication(WorkCreateViewModel work)
        {
            try
            {
                Work newWork = new Work();
                newWork.Body = work.Body;
                newWork.Genre = work.Genre;
                newWork.CoverPhoto = work.CoverPhoto;
                newWork.Title = work.Title;

                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    db.Works.Add(newWork);
                    db.SaveChanges();


                    return new HttpResponseMessage() { Content = new JsonContent(new { Success = true, data = newWork.Id }) };
                }



            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new JsonContent(new { Success = false, Message = ex.Message }) };

            }
        }


        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage GetAllWorks()
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
