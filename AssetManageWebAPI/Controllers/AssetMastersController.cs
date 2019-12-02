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
    public class AssetMastersController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();
        static decimal count;
        AssetMastersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/AssetMasters
       /* public IQueryable<AssetMaster> GetAssetMasters()
        {
            return db.AssetMasters;
        }*/

        // GET: api/AssetMasters/5
        [ResponseType(typeof(AssetMaster))]
        public IHttpActionResult GetAssetMaster(int id)
        {
            AssetMaster assetMaster = db.AssetMasters.Find(id);
            if (assetMaster == null)
            {
                return NotFound();
            }

            return Ok(assetMaster);
        }
        // GET: api/AssetMaster
        public List<AssetMasterViewModel> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<AssetMaster> amList = db.AssetMasters.ToList();
            List<AssetMasterViewModel> amvList = amList.Select(x => new AssetMasterViewModel
            {
                am_id = x.am_id,
                am_ad_id = x.am_ad_id,
                am_ad_name = x.AssetDef.ad_name,
                am_atype_id = x.am_atype_id,
                am_atype_name = x.Assettype.at_name,
                am_from = x.am_from,
                am_to = x.am_to,
                am_make_id = x.am_make_id,
                am_make_name = x.VendorCreation.vd_name,
                am_model = x.am_model,
                am_myyear = Convert.ToString(x.am_myear),
                am_pdate = Convert.ToString(x.am_pdate),
                am_snumber = x.am_snumber,
                am_warranty = x.am_warranty

            }).ToList();
            return amvList;
        }

        // PUT: api/AssetMasters/5
        /*[ResponseType(typeof(void))]
        public IHttpActionResult PutAssetMaster(int id, AssetMaster assetMaster)
        {
            
            if (id != assetMaster.am_id)
            {
                return BadRequest();
            }

            db.Entry(assetMaster).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetMasterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/
        // PUT: api/AssetMaster/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase_order(int id, PurchaseOrder purchase_order)
        {
            count = Convert.ToDecimal(purchase_order.pd_qty);
            db.Entry(purchase_order).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AssetMasters
        [ResponseType(typeof(AssetMaster))]
        public IHttpActionResult PostAssetMaster(AssetMaster assetMaster)
        {
            for (int i = 0; i < count; i++)
            {
                int min = 1000;
                int max = 9999;
                Random rdm = new Random();
                int id = rdm.Next(min, max);
                assetMaster.am_snumber = id.ToString();
                db.AssetMasters.Add(assetMaster);
                db.SaveChanges();
            }

            

            return CreatedAtRoute("DefaultApi", new { id = assetMaster.am_id }, assetMaster);
        }

        // DELETE: api/AssetMasters/5
        [ResponseType(typeof(AssetMaster))]
        public IHttpActionResult DeleteAssetMaster(int id)
        {
            AssetMaster assetMaster = db.AssetMasters.Find(id);
            if (assetMaster == null)
            {
                return NotFound();
            }

            db.AssetMasters.Remove(assetMaster);
            db.SaveChanges();

            return Ok(assetMaster);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AssetMasterExists(int id)
        {
            return db.AssetMasters.Count(e => e.am_id == id) > 0;
        }
    }
}