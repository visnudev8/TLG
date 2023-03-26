using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Learning.Models;

namespace Learning.Controllers
{
    public class mlregistrationsController : Controller
    {
        private xmlsystemEntities db = new xmlsystemEntities();

        // GET: mlregistrations
        public ActionResult Index()
        {
            return View(db.mlregistrations.ToList());
        }

        // GET: mlregistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mlregistration mlregistration = db.mlregistrations.Find(id);
            if (mlregistration == null)
            {
                return HttpNotFound();
            }
            return View(mlregistration);
        }

        // GET: mlregistrations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mlregistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UserName,EmailId,Password,Mobile")] mlregistration mlregistration)
        {
            if (ModelState.IsValid)
            {
                db.mlregistrations.Add(mlregistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mlregistration);
        }

        // GET: mlregistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mlregistration mlregistration = db.mlregistrations.Find(id);
            if (mlregistration == null)
            {
                return HttpNotFound();
            }
            return View(mlregistration);
        }

        // POST: mlregistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UserName,EmailId,Password,Mobile")] mlregistration mlregistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mlregistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mlregistration);
        }

        // GET: mlregistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mlregistration mlregistration = db.mlregistrations.Find(id);
            if (mlregistration == null)
            {
                return HttpNotFound();
            }
            return View(mlregistration);
        }

        // POST: mlregistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mlregistration mlregistration = db.mlregistrations.Find(id);
            db.mlregistrations.Remove(mlregistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateInput(false)]
        public FileResult Exportexcel(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
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
