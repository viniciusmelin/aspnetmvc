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

            ViewBag.classe = TempData["classe"];
            ViewBag.msg = TempData["msg"];
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
            SelectList amigos = new SelectList
                (
                    db.PessoaAmigo
                    .Include(a => a.PessoaFriends)
                    .Include(e => e.PessoaMe)
                    .Where(a => a.PessoaMe.ApplicationUserID == user.Id && a.PessoaFriendsId != a.PessoaMe.Id)
                    .Select(a => a.PessoaFriends), "Id", "Nome"
                );

            if (game.Count() == 0)
            {
                TempData["classe"] = "warning";
                TempData["msg"] = "Todos seus jogos estão emprestados ou não possui nenhum jogo!";
                return RedirectToAction("Index");
            }
            if (amigos.Count()==0)
            {
                TempData["classe"] = "warning";
                TempData["msg"] = "Você não possui nenhum amigo :( !";
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
            try
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

                    TempData["classe"] = "success";
                    TempData["msg"] = "Emprestimo realizado com sucesso!";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.PessoaFrindsId = new SelectList(db.PessoaAmigo.Include(a => a.PessoaFriends).Include(a => a.PessoaMe).Where(a => a.PessoaMe.ApplicationUser.Id == user.Id).Select(a => a.PessoaFriends), "Id", "Nome");
                ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
                return View(emprestimoModel);
            }
            catch (Exception)
            {
                TempData["classe"] = "error";
                TempData["msg"] = "Não foi possível cadastrar emprestimo";
                return RedirectToAction("Index");
            }

        }

        // GET: EmprestimoModels/Edit/5
        public ActionResult Edit(int? id)
        {
            try
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


                ViewBag.PessoaFrindsId = new SelectList(db.PessoaAmigo.Include(a => a.PessoaFriends).Where(a => a.PessoaMeId == 1).Select(a => a.PessoaFriends), "Id", "Nome");
                ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
                return View(emprestimoModel);
            }
            catch (Exception)
            {

                TempData["classe"] = "error";
                TempData["msg"] = "Não foi possível encontrar  emprestimo";
                return RedirectToAction("Index");
            }

        }

        // POST: EmprestimoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Game_id,Data_emprestimo,Data_devolucao,Data_devolvido")] EmprestimoModel emprestimoModel, int PessoaFrindsId)
        {
            try
            {
                PessoaModel pessoa = db.Pessoa.Where(e => e.ApplicationUserID == user.Id).First();
                emprestimoModel.PessoaSolicitante = db.Pessoa.Find(PessoaFrindsId);
                emprestimoModel.PessoaSolicitada = db.Pessoa.Find(pessoa.Id);
                emprestimoModel.Game = db.Game.Find(emprestimoModel.Game_id);
                emprestimoModel.Game_id = emprestimoModel.Game.GameId;
                emprestimoModel.Solicitado_id = emprestimoModel.PessoaSolicitada.Id;
                emprestimoModel.Solicitante_id = emprestimoModel.PessoaSolicitante.Id;

                if (ModelState.IsValid)
                {
                    db.Entry(emprestimoModel).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["classe"] = "success";
                    TempData["msg"] = "Jogo atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
                return View(emprestimoModel);
            }
            catch (Exception)
            {

                TempData["classe"] = "error";
                TempData["msg"] = "Não foi possível atualizar emprestimo!";
                return RedirectToAction("Index");
            }

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
            try
            {
                EmprestimoModel emprestimoModel = db.Emprestismo.Find(id);
                PessoaGameModel pessoaGame = db.PessoaGame.Include(e => e.Pessoa).Where(e => e.game_game_id == emprestimoModel.Game_id && e.Pessoa.ApplicationUserID == user.Id).First();
                db.Emprestismo.Remove(emprestimoModel);
                pessoaGame.Emprestado = false;

                db.Entry(pessoaGame).State = EntityState.Modified;
                TempData["classe"] = "success";
                TempData["msg"] = "Emprestimo excluído com sucesso!";
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["classe"] = "error";
                TempData["msg"] = "Não foi possível excluir emprestimo!";
                return RedirectToAction("Index");
            }

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
