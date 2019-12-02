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
    public class PurchaseOrdersController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();
        public PurchaseOrdersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/PurchaseOrders
        /*public IQueryable<PurchaseOrder> GetPurchaseOrders()
        {
            return db.PurchaseOrders;
        }*/

        // GET: api/PurchaseOrders/5
       /* [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult GetPurchaseOrder(decimal id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrder);
        }*/
        public List<PurchaseViewModel> GetPurchaseOrder()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<PurchaseOrder> apurchase = db.PurchaseOrders.ToList();
            List<PurchaseViewModel> aplist = apurchase.Select(x => new PurchaseViewModel
            {
                pd_id = Convert.ToInt32(x.pd_id),
                pd_order_no = x.pd_order_no,
                assetname = x.AssetDef.ad_name,
                assettype = x.Assettype.at_name,
                vendorname = x.VendorCreation.vd_name,
                pd_qty = Convert.ToInt32 (x.pd_qty),
                pd_odateStr = x.pd_odateStr,
                pd_ddateStr=x.pd_ddateStr,
                pd_status=x.pd_status
            }
            ).ToList();
            return aplist;
        }
       
        public List<VendorViewModel> GetPurchaseOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;

            List<VendorCreation> vlist = db.VendorCreations.Where(x => x.vd_atype_id == id).ToList();
            List<VendorViewModel> vvlist = vlist.Select(x => new VendorViewModel
            {
                vd_id = Convert.ToInt32(x.vd_id),
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id = x.Assettype.at_name,
                vd_fromStr = x.vd_fromStr,
                vd_toStr = x.vd_toStr,
                vd_addr = x.vd_addr

            }).ToList();
            return vvlist;
            }
        //get
        public List<Assettype> GetAssetTypes(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            AssetDef assetlist = db.AssetDefs.Where(x => x.ad_name == name).FirstOrDefault();
            List<Assettype> atlist = new List<Assettype>();
            if(assetlist!=null)
            {
                atlist= db.Assettypes.Where(x => x.at_id == assetlist.ad_type_id).ToList();
            }
            
            return atlist;
        }
        // PUT: api/PurchaseOrders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrder(decimal id, PurchaseOrder purchaseOrder)
        {
            

            if (id != purchaseOrder.pd_id)
            {
                return BadRequest();
            }

            db.Entry(purchaseOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
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

        // POST: api/PurchaseOrders
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {

            purchaseOrder.pd_odate = DateTime.Now;
            db.PurchaseOrders.Add(purchaseOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrder.pd_id }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult DeletePurchaseOrder(decimal id)
        {
            PurchaseOrder purchaseOrder = db.PurchaseOrders.Find(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            db.PurchaseOrders.Remove(purchaseOrder);
            db.SaveChanges();

            return Ok(purchaseOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrderExists(decimal id)
        {
            return db.PurchaseOrders.Count(e => e.pd_id == id) > 0;
        }
    }
}