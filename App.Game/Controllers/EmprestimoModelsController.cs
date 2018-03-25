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
    public class EmprestimoModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmprestimoModels
        public ActionResult Index()
        {
            var emprestismo = db.Emprestismo.Include(e => e.Game);
            return View(emprestismo.ToList());
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
            //  IEnumerable<GameModel> c = db.PessoaGame.Where(e => e.pessoa_pessoa_id == pessoa.Id).Select(e => e.Game);
            PessoaModel pessoa = db.Pessoa.Where(d => d.Id == 1).First();

            ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao");
            //PessoaModel pessoa1 = db.Pessoa.Where(d => d.Id == 2).First();
            //IList<PessoaModel> dada = db.Pessoa.Where(c => c.PessoaFrinds.Any(y => y.PessoaFrinds == pessoa)).ToList();
            IEnumerable<PessoaModel> query = pessoa.PessoaFriends.ToList();
            EmprestimoViewModel emp = new EmprestimoViewModel();
            emp.EmprestimoModel = new EmprestimoModel();
            emp.Amigo = new PessoaModel();
            ViewBag.Amigo_id = new SelectList(query,"Id","Nome");
            return View(emp);
        }

        // POST: EmprestimoModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Game_id,Data_emprestimo,Data_devolucao,Data_devolvido,")] EmprestimoViewModel emprestimoModel)
        {
            if (ModelState.IsValid)
            {
                db.Emprestismo.Add(emprestimoModel.EmprestimoModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.EmprestimoModel.Game_id);
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
            ViewBag.Game_id = new SelectList(db.Game, "GameId", "Descricao", emprestimoModel.Game_id);
            return View(emprestimoModel);
        }

        // POST: EmprestimoModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Game_id,Data_emprestimo,Data_devolucao,Data_devolvido")] EmprestimoModel emprestimoModel)
        {
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
            db.Emprestismo.Remove(emprestimoModel);
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
