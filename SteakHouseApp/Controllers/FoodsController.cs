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
    public class FoodsController : Controller
    {
        private SteakHouseEntities db = new SteakHouseEntities();

        // GET: Foods
        public ActionResult Index()
        {
            var foods = db.Foods.Include(f => f.Category).Include(f => f.Chefe);
            return View(foods.ToList());
        }

        // GET: Foods/Details/5
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Chefe_Id = new SelectList(db.Chefes, "Id", "Name");
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                upload.SaveAs(path);
                food.Image = upload.FileName;

                db.Foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", food.Category_Id);
            ViewBag.Chefe_Id = new SelectList(db.Chefes, "Id", "Name", food.Chefe_Id);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", food.Category_Id);
            ViewBag.Chefe_Id = new SelectList(db.Chefes, "Id", "Name", food.Chefe_Id);
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Food food, HttpPostedFileBase upload)
        {
            ViewBag.Category_Id = new SelectList(db.Categories, "Id", "Name", food.Category_Id);
            ViewBag.Chefe_Id = new SelectList(db.Chefes, "Id", "Name", food.Chefe_Id);
            if (ModelState.IsValid)
            {
                string oldpath = Path.Combine(Server.MapPath("~/images"), food.Image);
                if (upload != null)
                {
                    System.IO.File.Delete(oldpath);
                    string newpath = Path.Combine(Server.MapPath("~/images"), upload.FileName);
                    upload.SaveAs(newpath);
                    food.Image = upload.FileName;
                }
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Food food = db.Foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Foods.Find(id);
            db.Foods.Remove(food);
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
