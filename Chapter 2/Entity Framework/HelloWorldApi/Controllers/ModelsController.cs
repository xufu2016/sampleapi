using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HelloWorldApi.Models;

namespace HelloWorldApi.Controllers
{
    public class ModelsController : ApiController
    {
        private HelloWorldApiContext db = new HelloWorldApiContext();

        // GET: api/Models
        public IQueryable<Model> GetModels()
        {
            return db.Models;
        }

        // GET: api/Models/5
        [ResponseType(typeof(Model))]
        public IHttpActionResult GetModel(int id)
        {
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        // PUT: api/Models/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModel(int id, Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.ModelId)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Models
        [ResponseType(typeof(Model))]
        public IHttpActionResult PostModel(Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Models.Add(model);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = model.ModelId }, model);
        }

        // DELETE: api/Models/5
        [ResponseType(typeof(Model))]
        public IHttpActionResult DeleteModel(int id)
        {
            Model model = db.Models.Find(id);
            if (model == null)
            {
                return NotFound();
            }

            db.Models.Remove(model);
            db.SaveChanges();

            return Ok(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModelExists(int id)
        {
            return db.Models.Count(e => e.ModelId == id) > 0;
        }
    }
}