using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc; 
namespace ClockUniverse.Controllers
{
    public class HomeController : Controller
    {
        private CsK23T3bEntities db = new CsK23T3bEntities();
        public ActionResult Index(int? page)
        {
            // Tao  bien so san pham tren trang
            int pageSize = 12;
            // Tao bien so trang
            int pageNumber = (page ?? 1);
            return View(db.ProductTables.ToList().OrderByDescending(n=>n.Watch_ID).ToPagedList(pageNumber,pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search(string text)
        {
            var itemsz = db.ProductTables.Where(x => x.Watch_Name.ToLower().Contains(text.ToLower())).ToList();
            if (text.Trim().Equals(""))
            {
                return RedirectToAction("Index");
            }

            else if(itemsz.Count() > 0)
            {
                //ViewBag.Message = "";
            }
            else
            {
                ViewBag.Message = "Không tìm thấy được sản phẩm tương ứng";

            }
            ViewData["Item"] = itemsz;
            return View(itemsz);
        }
    }
}