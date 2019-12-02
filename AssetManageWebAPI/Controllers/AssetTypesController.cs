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
    public class AssetTypesController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();

        public AssetTypesController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Assettypes
        public IQueryable<Assettype> GetAssettypes()
        {
            return db.Assettypes;
        }
        /*public List<Assettype> getAssettypes(string na)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<AssetDef> ad = db.AssetDefs.Where(x => x.ad_name == na).ToList();
            List<Assettype> atlist = ad.Select(x => new Assettype
            {
                at_id = Convert.ToInt32(x.ad_type_id),
                at_name = x.Assettype.at_name
            }).ToList();
            return atlist;
        }*/

        // GET: api/Assettypes/5
        [ResponseType(typeof(Assettype))]
        public IHttpActionResult GetAssettype(decimal id)
        {
            Assettype assettype = db.Assettypes.Find(id);
            if (assettype == null)
            {
                return NotFound();
            }

            return Ok(assettype);
        }

        // PUT: api/Assettypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssettype(decimal id, Assettype assettype)
        {
           

            if (id != assettype.at_id)
            {
                return BadRequest();
            }

            db.Entry(assettype).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssettypeExists(id))
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

        // POST: api/Assettypes
        [ResponseType(typeof(Assettype))]
        public IHttpActionResult PostAssettype(Assettype assettype)
        {
            

            db.Assettypes.Add(assettype);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assettype.at_id }, assettype);
        }

        // DELETE: api/Assettypes/5
        [ResponseType(typeof(Assettype))]
        public IHttpActionResult DeleteAssettype(decimal id)
        {
            Assettype assettype = db.Assettypes.Find(id);
            if (assettype == null)
            {
                return NotFound();
            }

            db.Assettypes.Remove(assettype);
            db.SaveChanges();

            return Ok(assettype);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssettypeExists(decimal id)
        {
            return db.Assettypes.Count(e => e.at_id == id) > 0;
        }
    }
}