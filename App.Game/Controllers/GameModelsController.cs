using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Game.Models;
using App.Game.repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace App.Game.Controllers
{
    public class GameModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       private ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        // GET: GameModels
        public ActionResult Index()
        {
            PessoaModel pessoa = db.Pessoa.Where(d=> d.ApplicationUserID == user.Id).First();

            //PessoaModel pessoa1 = db.Pessoa.Where(d => d.Id == 2).First();

            //var teste = from d in db.PessoaGame from p in db.Game where d.pessoa_pessoa_id == pessoa.Id select d; 
            //ICollection<GameModel> lista = db.Game.Where(t => t.PessoaGame.pessoa_pessoa_id == pessoa.Id).ToList();
            ViewBag.Title = "Inicial";

           // EmprestimoModel emp = new EmprestimoModel();
           // PessoaModel pessoa3 = new PessoaModel();
           // pessoa.PessoaMe.Add(pessoa1);
           //// pessoa3.PessoaFrinds.Add(pessoa1);
           // pessoa.PessoaFrinds.Add(pessoa);
            // db.Pessoa.Add(pessoa3);
            IEnumerable<GameModel> c = db.PessoaGame.Where(e => e.pessoa_pessoa_id == pessoa.Id).Select(e => e.Game);

            return View(c.ToList());
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
                PessoaModel pessoa = db.Pessoa.Where(c => c.ApplicationUserID == user.Id).First();
                db.Game.Add(gameModel);
                PessoaGameModel pessoaGame = new PessoaGameModel();
                //db.SaveChangesAsync();
                pessoaGame.Pessoa = pessoa;
                pessoaGame.Game = gameModel;
                pessoaGame.game_game_id = gameModel.GameId;
                pessoaGame.pessoa_pessoa_id = pessoa.Id;
                db.PessoaGame.Add(pessoaGame);
                db.SaveChanges();




                var teste = gameModel.GameId;

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
