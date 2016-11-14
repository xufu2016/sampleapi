using System.Linq;
using HelloWorldApi.Models;
using System.Web.Http.OData;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Data.OData;
using System.Net;
using System.Data.Entity;
using System;

namespace HelloWorldApi.Controllers
{
    public class ModelsController : EntitySetController<Model, int>
    {
        private HelloWorldApiContext db = new HelloWorldApiContext();

        [EnableQuery(PageSize =10)]
        public override IQueryable<Model> Get()
        {
            return db.Models.AsQueryable();
        }

        protected override Model GetEntityByKey(int key)
        {
            var model =  db.Models.Find(key);
            if (model == null)
                throw Helpers.CreateODataHttpResponseException(Request, "Resource Not Found", HttpStatusCode.NotFound);

            return model;
        }

        public override HttpResponseMessage Post([FromBody] Model entity)
        {
            db.Models.Add(entity);
            db.SaveChanges();
            return Request.CreateResponse(entity);
        }

        public override HttpResponseMessage Patch([FromODataUri] int key, Delta<Model> patch)
        {
            var model = db.Models.Find(key);
            if (model == null)
                throw Helpers.CreateODataHttpResponseException(Request, "Resource Not Found", HttpStatusCode.NotFound);

            patch.Patch(model);
            db.SaveChanges();
            return Request.CreateResponse(model);
        }

        public override HttpResponseMessage Put([FromODataUri] int key, [FromBody] Model update)
        {
            var model = db.Models.Find(key);
            if (model == null)
                throw Helpers.CreateODataHttpResponseException(Request, "Resource Not Found", HttpStatusCode.NotFound);

            db.Entry(model).State = EntityState.Detached;
            db.Models.Attach(update);
            db.Entry(update).State = EntityState.Modified;
            db.SaveChanges();
            return Request.CreateResponse(update);
        }

        public override void Delete([FromODataUri] int key)
        {
            var model = db.Models.Find(key);
            if (model == null)
                throw Helpers.CreateODataHttpResponseException(Request, "Resource Not Found", HttpStatusCode.NotFound);

            db.Models.Remove(model);
            db.SaveChanges();
        }
    }

    public static class Helpers
    {
        public static HttpResponseException CreateODataHttpResponseException(HttpRequestMessage request, string message, HttpStatusCode statusCode)
        {
            HttpResponseException httpException;
            HttpResponseMessage response;
            ODataError error;

            error = new ODataError
            {
                Message = message,
                ErrorCode = Enum.GetName(typeof(HttpStatusCode), statusCode)
            };

            response = request.CreateResponse(statusCode, error);

            httpException = new HttpResponseException(response);

            return httpException;
        }
    }
}