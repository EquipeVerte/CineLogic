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
using CineLogic.Models;

namespace CineLogic.Controllers
{
    public class CinemasAPIController : ApiController
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: api/CinemasAPI
        public IQueryable<Cinema> GetCinemas()
        {
            return db.Cinemas;
        }

        // GET: api/CinemasAPI/5
        [ResponseType(typeof(Cinema))]
        public IHttpActionResult GetCinema(int id)
        {
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema);
        }

        // PUT: api/CinemasAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCinema(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cinema.CinemaID)
            {
                return BadRequest();
            }

            db.Entry(cinema).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExists(id))
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

        // POST: api/CinemasAPI
        [ResponseType(typeof(Cinema))]
        public IHttpActionResult PostCinema(Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cinemas.Add(cinema);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cinema.CinemaID }, cinema);
        }

        // DELETE: api/CinemasAPI/5
        [ResponseType(typeof(Cinema))]
        public IHttpActionResult DeleteCinema(int id)
        {
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return NotFound();
            }

            db.Cinemas.Remove(cinema);
            db.SaveChanges();

            return Ok(cinema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CinemaExists(int id)
        {
            return db.Cinemas.Count(e => e.CinemaID == id) > 0;
        }
    }
}