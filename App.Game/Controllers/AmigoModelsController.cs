using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Game.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace App.Game.Controllers
{
    public class AmigoModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: AmigoModels
        public ActionResult Index()
        {
            ViewBag.classe = TempData["classe"];
            ViewBag.msg = TempData["msg"];
            var pessoaAmigo = db.PessoaAmigo.Include(a => a.PessoaFriends).Include(a => a.PessoaMe).Where(e=>e.PessoaMe.ApplicationUserID == user.Id);
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
            
            ViewBag.PessoaFriendsId = new SelectList(db.Pessoa.Where(e => e.ApplicationUserID != user.Id), "Id", "Nome");
            ViewBag.PessoaMeId = new SelectList(db.Pessoa.Where(e=>e.ApplicationUser.Id == user.Id), "Id", "Nome");
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

                if (db.PessoaAmigo.Where(e => e.PessoaMe.Id == amigoModel.PessoaMeId && e.PessoaFriendsId == amigoModel.PessoaFriendsId).Count() > 0)
                {
                    ViewBag.PessoaFriendsId = new SelectList(db.Pessoa.Where(e => e.ApplicationUserID != user.Id), "Id", "Nome", amigoModel.PessoaFriends);
                    ViewBag.PessoaMeId = new SelectList(db.Pessoa.Where(e => e.ApplicationUser.Id == user.Id), "Id", "Nome", amigoModel.PessoaMe);

                    ViewBag.classe = "error";
                    ViewBag.msg = "Você já cdastrou este amigo!";
                    return View(amigoModel);
                }

                TempData["classe"] = "success";
                TempData["msg"] = "Amigo Cadastrado!";
                db.PessoaAmigo.Add(amigoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PessoaFriendsId = new SelectList(db.Pessoa.Where(e => e.ApplicationUserID != user.Id), "Id", "Nome",amigoModel.PessoaFriends);
            ViewBag.PessoaMeId = new SelectList(db.Pessoa.Where(e => e.ApplicationUser.Id == user.Id), "Id", "Nome",amigoModel.PessoaMe);
            return View(amigoModel);
        }

        // GET: AmigoModels/Edit/5
        public ActionResult Edit(int? id, int? idamigo )
        {

            TempData["classe"] = "warning";
            TempData["msg"] = "Visualização desabilidata";
            return RedirectToAction("index");
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
            TempData["classe"] = "success";
            TempData["msg"] = "Amigo Removido com sucesso!";
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
