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
    public class AssetMastersOrderViewController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();
        AssetMastersOrderViewController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/AssetMastersOrderView
        /* public IQueryable<AssetMaster> GetAssetMasters()
         {
             return db.AssetMasters;
         }*/

        // GET: api/AssetMastersOrderView/5
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
        // GET: api/AssetMasterOrderView
        public List<PurchaseViewModel> GetAsset_master()
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<PurchaseOrder> pList = db.PurchaseOrders.Where(x => x.pd_status == "Consignment Received").ToList();
            List<PurchaseViewModel> pvList = pList.Select(x => new PurchaseViewModel
            {
                pd_id = Convert.ToInt32(x.pd_id),
                pd_order_no = x.pd_order_no,
                pd_ad_id = x.AssetDef.ad_id,
                assetname = x.AssetDef.ad_name,
                pd_odateStr = x.pd_odateStr,
                pd_ddateStr = x.pd_ddateStr,
                pd_qty = Convert.ToInt32(x.pd_qty),
                pd_status = x.pd_status,
                pd_type_id = x.Assettype.at_id,
                assettype = x.Assettype.at_name,
                pd_vendor_id = x.pd_vendor_id,
                vendorname = x.VendorCreation.vd_name
            }).ToList();

            return pvList;
        }
        // GET: api/AssetMasterOrderView/5
        [ResponseType(typeof(AssetMaster))]
        public PurchaseViewModel GetAsset_master(string ordno)
        {
            db.Configuration.ProxyCreationEnabled = true;
            PurchaseOrder x = db.PurchaseOrders.Where(y => y.pd_order_no == ordno).FirstOrDefault();
            PurchaseViewModel pvModel = new PurchaseViewModel();

            if (x == null)
            {

            }

            else
            {
                pvModel.pd_id = Convert.ToInt32( x.pd_id);
                pvModel.pd_order_no = x.pd_order_no;
                pvModel.pd_ad_id = x.AssetDef.ad_id;
                pvModel.assetname = x.AssetDef.ad_name;
                pvModel.pd_odateStr = x.pd_odateStr;
                pvModel.pd_ddateStr = x.pd_ddateStr;
                pvModel.pd_qty = Convert.ToInt32(x.pd_qty);
                pvModel.pd_status = x.pd_status;
                pvModel.pd_type_id = x.Assettype.at_id;
                pvModel.assettype = x.Assettype.at_name;
                pvModel.pd_vendor_id = x.pd_vendor_id;
                pvModel.vendorname = x.VendorCreation.vd_name;
            }

            return pvModel;
        }
        // PUT: api/AssetMastersOrderView/5
        [ResponseType(typeof(void))]
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
        }

        // POST: api/AssetMastersOrderView
        [ResponseType(typeof(AssetMaster))]
        public IHttpActionResult PostAssetMaster(AssetMaster assetMaster)
        {
           
            db.AssetMasters.Add(assetMaster);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = assetMaster.am_id }, assetMaster);
        }

        // DELETE: api/AssetMastersOrderView/5
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