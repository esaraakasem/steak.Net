using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SteakHouseApp.Models;

namespace SteakHouseApp.Controllers
{
   
    public class ChefesController : Controller
    {
        private SteakHouseEntities db = new SteakHouseEntities();

        // GET: Chefes
        public ActionResult Index()
        {
            return View(db.Chefes.ToList());
        }

        // GET: Chefes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chefe chefe = db.Chefes.Find(id);
            if (chefe == null)
            {
                return HttpNotFound();
            }
            return View(chefe);
        }

        // GET: Chefes/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Chefes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chefe chefe,HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                upload.SaveAs(path);
                chefe.Image = upload.FileName;

                db.Chefes.Add(chefe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chefe);
        }

        // GET: Chefes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chefe chefe = db.Chefes.Find(id);
            if (chefe == null)
            {
                return HttpNotFound();
            }
            return View(chefe);
        }

        // POST: Chefes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chefe chefe, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/images"), chefe.Image);
                if (upload != null)
                {
                    System.IO.File.Delete(oldpath);
                    string newPath = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                    upload.SaveAs(newPath);
                    chefe.Image = upload.FileName;
                }
                db.Entry(chefe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chefe);
        }

        // GET: Chefes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chefe chefe = db.Chefes.Find(id);
            if (chefe == null)
            {
                return HttpNotFound();
            }
            return View(chefe);
        }

        // POST: Chefes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chefe chefe = db.Chefes.Find(id);
            db.Chefes.Remove(chefe);
            db.SaveChanges();
            return RedirectToAction("Index");
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
