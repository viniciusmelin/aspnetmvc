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
            ViewBag.classe = TempData["classe"];
            ViewBag.msg = TempData["msg"];
            ViewBag.Title = "Inicial";
            IEnumerable<GameModel> c = db.PessoaGame.Where(e => e.Pessoa.Id == pessoa.Id).Select(e => e.Game);


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
                PessoaGameModel pessoaGame = new PessoaGameModel
                {
                    Pessoa = pessoa,
                    Game = gameModel,
                    game_game_id = gameModel.GameId,
                    pessoa_pessoa_id = pessoa.Id
                };
                db.PessoaGame.Add(pessoaGame);
                db.SaveChanges();

                TempData["classe"] = "success";
                TempData["msg"] = "Jogo cadastrado com sucesso!";

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
                TempData["classe"] = "success";
                TempData["msg"] = "Jogo atualizado com sucesso!";
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
            
            
            var emprestado = (from p in db.Emprestismo where p.Game_id == gameModel.GameId select p).Count();

            if (emprestado > 0)
            {
                ModelState.AddModelError("Descricao", "Não é Possível Excluir, pois jogo esta emprestado!");
                
                return View(gameModel);
            }
            PessoaGameModel pessoagame = db.PessoaGame.Where(e => e.game_game_id == gameModel.GameId && e.pessoa_pessoa_id == 1).First();

            db.PessoaGame.Remove(pessoagame);
            db.Game.Remove(gameModel);
            db.SaveChanges();
            TempData["classe"] ="success";
            TempData["msg"] = "Jogo excluído com sucesso!";
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
