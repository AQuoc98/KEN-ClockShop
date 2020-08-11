using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace ClockUniverse.Controllers
{
    public class productsController : Controller
    {
        //
        CsK23T3bEntities db = new CsK23T3bEntities();
        public ActionResult listItem( int type)
        {
            // Tao  bien so san pham tren trang
            var model = db.ProductTables.Where(m => m.WatchType_ID == type);
         
            return View(model.OrderByDescending(n => n.Watch_ID));
        }
        // GET: /products/
        public ActionResult Index()
        {
            return View(db.ProductTables.ToList());
        }
        public ViewResult product_detail(int watch_ID = 0)
        {
            // Tra về đôi tượng với điều kiện
            ProductTable product = db.ProductTables.SingleOrDefault(n => n.Watch_ID == watch_ID);
            if (product == null)
            {
                // Trả về trang báo lỗi
                return View("~/Views/notfound/Index.cshtml");
            }
            return View(product);
        }

    }
}