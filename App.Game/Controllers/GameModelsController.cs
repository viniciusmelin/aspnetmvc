using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Game.Models;

namespace App.Game.Controllers
{
    public class GameModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GameModels
        public ActionResult Index()
        {
            ViewBag.Title = "Inicial";
            return View(db.Game.ToList());
        }

        // GET: GameModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameModel gameModel = db.Game.Find(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }

        // GET: GameModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameId,Descricao")] GameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                db.Game.Add(gameModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gameModel);
        }

        // GET: GameModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameModel gameModel = db.Game.Find(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }

        // POST: GameModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Descricao")] GameModel gameModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gameModel);
        }

        // GET: GameModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameModel gameModel = db.Game.Find(id);
            if (gameModel == null)
            {
                return HttpNotFound();
            }
            return View(gameModel);
        }

        // POST: GameModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameModel gameModel = db.Game.Find(id);
            db.Game.Remove(gameModel);
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
