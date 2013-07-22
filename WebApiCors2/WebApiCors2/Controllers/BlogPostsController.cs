using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiCors2.Models;

namespace WebApiCors2.Controllers
{
    [EnableCorsAttribute("http://www.dotnetcurry.com/", "*", "*")]
    public class BlogPostsController : ApiController
    {
        private WebApiCorsContext db = new WebApiCorsContext();

        // GET api/BlogPosts
        public IEnumerable<BlogPost> GetBlogPosts()
        {
            return db.BlogPosts.AsEnumerable();
        }

        // GET api/BlogPosts/5
        public BlogPost GetBlogPost(int id)
        {
            BlogPost blogpost = db.BlogPosts.Find(id);
            if (blogpost == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return blogpost;
        }

        // PUT api/BlogPosts/5
        public HttpResponseMessage PutBlogPost(int id, BlogPost blogpost)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != blogpost.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(blogpost).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/BlogPosts
        public HttpResponseMessage PostBlogPost(BlogPost blogpost)
        {
            if (ModelState.IsValid)
            {
                db.BlogPosts.Add(blogpost);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, blogpost);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = blogpost.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/BlogPosts/5
        public HttpResponseMessage DeleteBlogPost(int id)
        {
            BlogPost blogpost = db.BlogPosts.Find(id);
            if (blogpost == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BlogPosts.Remove(blogpost);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, blogpost);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}