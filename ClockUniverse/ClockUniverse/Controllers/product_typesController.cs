﻿using System.Linq;
using System.Web.Mvc;

namespace ClockUniverse.Controllers
{
    public class product_typesController : Controller
    {
        // Database
        CsK23T3bEntities db = new CsK23T3bEntities();
        // GET: /product_types/
        public ActionResult Index()
        {
            return PartialView(db.ProductTypes.Take(5).ToList());
        }

        public ViewResult ListType()
        {
            return View(db.ProductTypes.ToList());
        }
	}
}