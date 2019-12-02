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
using AssetManageWebAPI.Models;

namespace AssetManageWebAPI.Controllers
{
    public class UserTblsController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();

        // GET: api/usertbls
        public IQueryable<usertbl> Getusertbls()
        {
            return db.usertbls;
        }

        // GET: api/usertbls/5
        [ResponseType(typeof(usertbl))]
        public IHttpActionResult Getusertbl(int id)
        {
            usertbl usertbl = db.usertbls.Find(id);
            if (usertbl == null)
            {
                return NotFound();
            }

            return Ok(usertbl);
        }

        // PUT: api/usertbls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putusertbl(int id, usertbl usertbl)
        {
            
            if (id != usertbl.ut_id)
            {
                return BadRequest();
            }

            db.Entry(usertbl).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usertblExists(id))
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

        // POST: api/usertbls
        [ResponseType(typeof(usertbl))]
        public IHttpActionResult Postusertbl(usertbl usertbl)
        {
           

            db.usertbls.Add(usertbl);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = usertbl.ut_id }, usertbl);
        }

        // DELETE: api/usertbls/5
        [ResponseType(typeof(usertbl))]
        public IHttpActionResult Deleteusertbl(int id)
        {
            usertbl usertbl = db.usertbls.Find(id);
            if (usertbl == null)
            {
                return NotFound();
            }

            db.usertbls.Remove(usertbl);
            db.SaveChanges();

            return Ok(usertbl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool usertblExists(int id)
        {
            return db.usertbls.Count(e => e.ut_id == id) > 0;
        }
    }
}