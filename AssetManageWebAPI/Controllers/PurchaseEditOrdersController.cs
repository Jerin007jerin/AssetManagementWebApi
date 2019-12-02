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
    public class PurchaseEditOrdersController : ApiController
    {
        private assetDBEntities1 db = new assetDBEntities1();
        public PurchaseEditOrdersController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/PurchaseEditOrders
        public IQueryable<PurchaseOrder> GetPurchaseOrders()
        {
            return db.PurchaseOrders;
        }

        // GET: api/PurchaseEditOrders/5
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

        //get
        public PurchaseViewModel GetPurchaseOrder(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            PurchaseOrder order = db.PurchaseOrders.Where(x => x.pd_id == id).FirstOrDefault();
            PurchaseViewModel model = new PurchaseViewModel();
            model.pd_id = Convert.ToInt32(order.pd_id);
            model.pd_order_no = order.pd_order_no;
            model.assetname= order.AssetDef.ad_name;
            model.assettype = order.Assettype.at_name;
            model.pd_ad_id = order.pd_ad_id;
            model.pd_type_id = order.pd_type_id;
            model.pd_vendor_id = order.pd_vendor_id;
            model.vendorname = order.VendorCreation.vd_name;
            model.pd_qty = Convert.ToInt32(order.pd_qty);
            model.pd_odateStr = order.pd_odateStr;
            model.pd_ddateStr = order.pd_ddateStr;
            model.pd_status = order.pd_status;
            return model;
        }
        // PUT: api/PurchaseEditOrders/5
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

        // POST: api/PurchaseEditOrders
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            

            db.PurchaseOrders.Add(purchaseOrder);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrder.pd_id }, purchaseOrder);
        }

        // DELETE: api/PurchaseEditOrders/5
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