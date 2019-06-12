using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SteakHouseApp.Models;

namespace SteakHouseApp.Controllers
{
    public class HomeController : Controller
    {
        private SteakHouseEntities de = new SteakHouseEntities();
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.chefes = de.Chefes.ToList();
            ViewBag.Category = de.Categories.ToList();
            ViewBag.foods = de.Foods.Take(6);
            return View(de.Foods.ToList());
        }
        [HttpPost]
        public ActionResult Index(ContactU cantactU)
        {
            @ViewBag.messerror = " Please enter Fields correct";
            ViewBag.chefes = de.Chefes.ToList();
            ViewBag.Category = de.Categories.ToList();
            if (ModelState.IsValid)
                {
                    Session["name"] = cantactU.Name;
                    de.ContactUs.Add(cantactU);
                    de.SaveChanges();
                    return RedirectToAction("Contact");
                }
            return View(de.Foods.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = de.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Thank For Your Message " + Session["name"];

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login(SignUp log)
        {
            if (ModelState.IsValid)
            {
                var admin = de.SignUps.Where(a => a.Name == log.Name && a.Password == log.Password);
                if (admin!=null)
                {
                    return RedirectToAction("Index", "Categories");
                }
            }
            return View(log);
        }
    }
}