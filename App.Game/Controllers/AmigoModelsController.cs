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
    public class AmigoModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AmigoModels
        public ActionResult Index()
        {
            var pessoaAmigo = db.PessoaAmigo.Include(a => a.PessoaFriends).Include(a => a.PessoaMe).Where(e=>e.PessoaMeId ==1);
            return View(pessoaAmigo.ToList());
        }

        // GET: AmigoModels/Details/5
        public ActionResult Details(int? id, int? idamigo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmigoModel amigoModel = db.PessoaAmigo.Find(id, idamigo);
            if (amigoModel == null)
            {
                return HttpNotFound();
            }
            return View(amigoModel);
        }

        // GET: AmigoModels/Create
        public ActionResult Create()
        {
            ViewBag.PessoaFriendsId = new SelectList(db.Pessoa, "Id", "Nome");
            ViewBag.PessoaMeId = new SelectList(db.Pessoa.Where(e=>e.Id ==1), "Id", "Nome");
            return View();
        }

        // POST: AmigoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PessoaMeId,PessoaFriendsId")] AmigoModel amigoModel)
        {
            if (ModelState.IsValid)
            {
                db.PessoaAmigo.Add(amigoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PessoaFriendsId = new SelectList(db.Pessoa, "Id", "Nome", amigoModel.PessoaFriendsId);
            ViewBag.PessoaMeId = new SelectList(db.Pessoa, "Id", "Nome", amigoModel.PessoaMeId);
            return View(amigoModel);
        }

        // GET: AmigoModels/Edit/5
        public ActionResult Edit(int? id, int? idamigo )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmigoModel amigoModel = db.PessoaAmigo.Find(id,idamigo);
            if (amigoModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.PessoaFriendsId = new SelectList(db.Pessoa, "Id", "Nome", amigoModel.PessoaFriendsId);
            ViewBag.PessoaMeId = new SelectList(db.Pessoa, "Id", "Nome", amigoModel.PessoaMeId);
            return View(amigoModel);
        }

        // POST: AmigoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PessoaMeId,PessoaFriendsId")] AmigoModel amigoModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amigoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PessoaFriendsId = new SelectList(db.Pessoa, "Id", "Nome", amigoModel.PessoaFriendsId);
            ViewBag.PessoaMeId = new SelectList(db.Pessoa, "Id", "Nome", amigoModel.PessoaMeId);
            return View(amigoModel);
        }

        // GET: AmigoModels/Delete/5
        public ActionResult Delete(int? id, int ? idamigo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmigoModel amigoModel = db.PessoaAmigo.Find(id, idamigo);
            if (amigoModel == null)
            {
                return HttpNotFound();
            }
            return View(amigoModel);
        }

        // POST: AmigoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? idamigo)
        {
            AmigoModel amigoModel = db.PessoaAmigo.Find(id,idamigo);
            db.PessoaAmigo.Remove(amigoModel);
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
