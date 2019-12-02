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
    public class VendorCreationsController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();
        public VendorCreationsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/VendorCreations
       // public IQueryable<VendorCreation> GetVendorCreations()
        //{
        //    return db.VendorCreations;
        //}

        // GET: api/VendorCreations/5
        [ResponseType(typeof(VendorCreation))]
        public IHttpActionResult GetVendorCreation(decimal id)
        {
            VendorCreation vendorCreation = db.VendorCreations.Find(id);
            if (vendorCreation == null)
            {
                return NotFound();
            }

            return Ok(vendorCreation);
        }
        public List<VendorViewModel> GetVendorCreation()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<VendorCreation> avend = db.VendorCreations.ToList();
            List<VendorViewModel> avlist = avend.Select(x => new VendorViewModel
            {
                vd_id =Convert.ToInt32(x.vd_id),
                vd_name=x.vd_name,
                vd_type=x.vd_type,
                vd_atype_id = x.Assettype.at_name,
                vd_fromStr=x.vd_fromStr,
                vd_toStr= x.vd_toStr,
                vd_addr=x.vd_addr
            }
            ).ToList();
            return avlist;
        }

        // PUT: api/VendorCreations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendorCreation(decimal id, VendorCreation vendorCreation)
        {
            

            if (id != vendorCreation.vd_id)
            {
                return BadRequest();
            }

            db.Entry(vendorCreation).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorCreationExists(id))
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

        // POST: api/VendorCreations
        [ResponseType(typeof(VendorCreation))]
        public int PostVendorCreation(VendorCreation vendorCreation)
        {


            /*db.VendorCreations.Add(vendorCreation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendorCreation.vd_id }, vendorCreation);*/
            VendorCreation vendor = new VendorCreation();
            vendor = db.VendorCreations.Where(x => x.vd_name ==vendorCreation.vd_name && x.vd_atype_id==vendorCreation.vd_atype_id).FirstOrDefault();
            if (vendor == null)
            {
                db.VendorCreations.Add(vendorCreation);
                db.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // DELETE: api/VendorCreations/5
        [ResponseType(typeof(VendorCreation))]
        public IHttpActionResult DeleteVendorCreation(int id)
        {
            VendorCreation vendorCreation = db.VendorCreations.Find(id);
            if (vendorCreation == null)
            {
                return NotFound();
            }

            db.VendorCreations.Remove(vendorCreation);
            db.SaveChanges();

            return Ok(vendorCreation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorCreationExists(decimal id)
        {
            return db.VendorCreations.Count(e => e.vd_id == id) > 0;
        }
    }
}