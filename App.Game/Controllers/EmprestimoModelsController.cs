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
    public class EmprestimoModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: EmprestimoModels
        public ActionResult Index()
        {


            var emprestimo = db.Emprestismo.Include(e => e.PessoaSolicitada).Where(e => e.PessoaSolicitada.ApplicationUserID == user.Id);
            return View(emprestimo.ToList());
        }

        // GET: EmprestimoModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmprestimoModel emprestimoModel = db.Emprestismo.Find(id);
            if (emprestimoModel == null)
            {
                return HttpNotFound();
            }
            return View(emprestimoModel);
        }

        // GET: EmprestimoModels/Create
        public ActionResult Create()
        {

            SelectList game = new SelectList(db.PessoaGame.Include(e => e.Game).Include(e => e.Pessoa).Where(e => e.Pessoa.ApplicationUserID == user.Id && e.Emprestado == false).Select(e => e.Game), "GameId", "Descricao");
            SelectList amigos = new SelectList(db.PessoaAmigo.Include(a => a.PessoaFriends).Include(e => e.PessoaMe).Where(a => a.PessoaMe.ApplicationUserID == user.Id).Select(a => a.PessoaFriends), "Id", "Nome");

            if (game.Count() == 0)
            {
                return RedirectToAction("Index");
            }
            if (amigos.Count()==0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.PessoaFrindsId = amigos;
            ViewBag.Game_id = game;
            return View();
        }

        // POST: EmprestimoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Game_id,Data_emprestimo,Data_devolucao,Data_devolvido")] EmprestimoModel emprestimoModel, int PessoaFrindsId)
        {
            if (ModelState.IsValid)
            {

                emprestimoModel.PessoaSolicitante = db.Pessoa.Find(PessoaFrindsId);
                emprestimoModel.PessoaSolicitada = db.Pessoa.Where(e => e.ApplicationUserID == user.Id).First();
                db.Emprestismo.Add(emprestimoModel);
                PessoaGameModel emprestado = new PessoaGameModel()
                {
                    pessoa_pessoa_id = emprestimoModel.PessoaSolicitada.Id,
                    game_game_id = emprestimoModel.Game_id,
                    Emprestado = true
                };
                db.Entry(emprestado).State = EntityState.Modified;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //dei um include pessoame caso erro retirar
            ViewBag.PessoaFrindsId = new SelectList(db.PessoaAmigo.Include(a => a.PessoaFriends).Include(a=>a.PessoaMe).Where(a => a.PessoaMe.ApplicationUser.Id == user.Id).Select(a => a.PessoaFriends), "Id", "Nome");
            ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
            return View(emprestimoModel);
        }

        // GET: EmprestimoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmprestimoModel emprestimoModel = db.Emprestismo.Find(id);
            if (emprestimoModel == null)
            {
                return HttpNotFound();
            }

            // PessoaModel pessoa = db.Pessoa.Where(d => d.Id == 1).First();
            ViewBag.PessoaFrindsId = new SelectList(db.PessoaAmigo.Include(a => a.PessoaFriends).Where(a => a.PessoaMeId == 1).Select(a => a.PessoaFriends), "Id", "Nome");
            ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
            return View(emprestimoModel);
        }

        // POST: EmprestimoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Game_id,Data_emprestimo,Data_devolucao,Data_devolvido")] EmprestimoModel emprestimoModel, int PessoaFrindsId)
        {
            emprestimoModel.PessoaSolicitante = db.Pessoa.Find(PessoaFrindsId);
            emprestimoModel.PessoaSolicitada = db.Pessoa.Find(1);
            emprestimoModel.Game = db.Game.Find(emprestimoModel.Game_id);
            emprestimoModel.Game_id = emprestimoModel.Game.GameId;
            emprestimoModel.Solicitado_id = emprestimoModel.PessoaSolicitada.Id;
            emprestimoModel.Solicitante_id = emprestimoModel.PessoaSolicitante.Id;

            if (ModelState.IsValid)
            {
                db.Entry(emprestimoModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
            return View(emprestimoModel);
        }

        // GET: EmprestimoModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmprestimoModel emprestimoModel = db.Emprestismo.Find(id);
            if (emprestimoModel == null)
            {
                return HttpNotFound();
            }
            return View(emprestimoModel);
        }

        // POST: EmprestimoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmprestimoModel emprestimoModel = db.Emprestismo.Find(id);
            PessoaGameModel pessoaGame = db.PessoaGame.Include(e => e.Pessoa).Where(e => e.game_game_id == emprestimoModel.Game_id && e.Pessoa.ApplicationUserID == user.Id).First();
            db.Emprestismo.Remove(emprestimoModel);
            pessoaGame.Emprestado = false;
            db.Entry(pessoaGame).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost,ActionName("CadastarAmigo")]
        //[ValidateAntiForgeryToken]
        public ActionResult Amigo()
        {

            return View("Amigo");
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
