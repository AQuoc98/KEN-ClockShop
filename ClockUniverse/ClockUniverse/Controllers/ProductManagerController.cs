﻿using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Transactions;
using System;


namespace ClockUniverse.Controllers
{
     
    public class ProductManagerController : Controller
    {
        private CsK23T3bEntities db = new CsK23T3bEntities();

        // GET: /ProductManager/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.ProductTables.ToList());
        }

        // GET: /ProductManager/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            ProductTable producttable = db.ProductTables.Find(id);
            if (producttable == null)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(producttable);
        }

        // GET: /ProductManager/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name");
            return View();
        }

        // POST: /ProductManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Watch_ID,Watch_Name,Watch_Description,Watch_Static,WatchType_ID,Original_Price,Selling_Price,InStock")] ProductTable producttable)
        {

            ValidateClock(producttable);


            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {

                    // add model to database
                    db.ProductTables.Add(producttable);
                    db.SaveChanges();
                    // save file to app_data
                    var path = Server.MapPath("~/App_Data");
                    path = System.IO.Path.Combine(path, producttable.Watch_ID.ToString());
                    Request.Files["Image"].SaveAs(path + "_0");
                    Request.Files["Image1"].SaveAs(path + "_1");
                    Request.Files["Image2"].SaveAs(path + "_2");
                    // all done successfully
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", producttable.WatchType_ID);
            return View("Create", producttable);

        }

        // GET: /ProductManager/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            ProductTable producttable = db.ProductTables.Find(id);
            if (producttable == null)
            {
                return RedirectToAction("index", "notfound");
            }
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", producttable.WatchType_ID);
            return View(producttable);
        }

        // POST: /ProductManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductTable producttable)
        {
            ValidateClock(producttable);
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope())
                {
                    // add model to database
                    db.Entry(producttable).State = EntityState.Modified;
                    db.SaveChanges();
                    // save file to app_data
                    var path = Server.MapPath("~/App_Data");
                    path = System.IO.Path.Combine(path, producttable.Watch_ID.ToString());

                    if (Request.Files["Image"].ContentLength != 0)

                    {
                        Request.Files["Image"].SaveAs(path + "_0");
                    }
                    if(Request.Files["Image1"].ContentLength != 0)
                    {
                        Request.Files["Image1"].SaveAs(path + "_1");
                    }
                    if(Request.Files["Image2"].ContentLength != 0)
                    {
                        Request.Files["Image2"].SaveAs(path + "_2");
                    }
                    // all done successfully
                    scope.Complete();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.WatchType_ID = new SelectList(db.ProductTypes, "ProductType_ID", "ProductType_Name", producttable.WatchType_ID);
            return View("Edit", producttable);
        }
       
        public ActionResult Image(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_0", "image/*");

        }
        
        public ActionResult Image1(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_1", "image/*");

        }
      
        public ActionResult Image2(string id)
        {
            var path = Server.MapPath("~/App_Data");
            path = System.IO.Path.Combine(path, id);
            return File(path + "_2", "image/*");

        }

        private void ValidateClock(ProductTable model)
        {
            if (model.Original_Price == 0)
                ModelState.AddModelError("Original_Price", Resource1.priceLess0);
            
        }

        // GET: /ProductManager/Delete/5 //
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "notfound");
            }
            ProductTable producttable = db.ProductTables.Find(id);
            if (producttable == null)
            {
                return RedirectToAction("index", "notfound");
            }
            return View(producttable);
        }

        // POST: /ProductManager/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductTable producttable = db.ProductTables.Find(id);

            var rs = db.Order_Detail.Where(p => p.Watch_ID == id);
            if (rs.Count() != 0)
            {
                ModelState.AddModelError("Watch_ID", "Sản phẩm này đang được sử dụng");
            }
            else
            {
                db.ProductTables.Remove(producttable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producttable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
