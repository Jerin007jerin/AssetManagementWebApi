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
    public class AssetDefsController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();
       public AssetDefsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/AssetDefs
       /* public IQueryable<AssetDef> GetAssetDefs()
        {
            return db.AssetDefs;
        }*/

        // GET: api/AssetDefs/5
       [ResponseType(typeof(AssetDef))]
        public IHttpActionResult GetAssetDef(decimal id)
        {
            AssetDef assetDef = db.AssetDefs.Find(id);
            if (assetDef == null)
            {
                return NotFound();
            }

            return Ok(assetDef);
        }
        public List<AssetViewModel> GetAssetDef()
        {
        db.Configuration.ProxyCreationEnabled = true;
            List<AssetDef> adef = db.AssetDefs.ToList();
            List<AssetViewModel> avlist = adef.Select(x => new AssetViewModel
            {
                ad_name=x.ad_name,
                ad_id=x.ad_id,
                ad_type_id=x.Assettype.at_name,
                ad_class=x.ad_class
            }
            ).ToList();
            return avlist;
        }
        //Get 
        public List<AssetDef> GetAssetDefs(string name)
        {
            List<AssetDef> alist = db.AssetDefs.Where(x => x.ad_name.StartsWith(name)).ToList();
            return alist;
        }

        // PUT: api/AssetDefs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAssetDef(decimal id, AssetDef assetDef)
        {
          

            if (id != assetDef.ad_id)
            {
                return BadRequest();
            }

            db.Entry(assetDef).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetDefExists(id))
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

        // POST: api/AssetDefs
        [ResponseType(typeof(AssetDef))]
        public int PostAssetDef(AssetDef assetDef)
        {
            /* db.AssetDefs.Add(assetDef);
             db.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { id = assetDef.ad_id }, assetDef);*/

            AssetDef asset = new AssetDef();
            asset = db.AssetDefs.Where(x => x.ad_name == assetDef.ad_name && x.ad_type_id == assetDef.ad_type_id && x.ad_class == assetDef.ad_class).FirstOrDefault();
            if(asset==null)
            {
                db.AssetDefs.Add(assetDef);
                db.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }

        // DELETE: api/AssetDefs/5
        [ResponseType(typeof(AssetDef))]
        public IHttpActionResult DeleteAssetDef(decimal id)
        {
            AssetDef assetDef = db.AssetDefs.Find(id);
            if (assetDef == null)
            {
                return NotFound();
            }

            db.AssetDefs.Remove(assetDef);
            db.SaveChanges();

            return Ok(assetDef);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetDefExists(decimal id)
        {
            return db.AssetDefs.Count(e => e.ad_id == id) > 0;
        }
    }
}