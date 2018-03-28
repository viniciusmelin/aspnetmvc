using App.Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace App.Game.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
         {
            IList<PessoaGameModel> pessoaGame = db.PessoaGame.Include(e => e.Pessoa).Include(e => e.Game).ToList();

            return View(pessoaGame);
        }

        public JsonResult Json()
        {
            var game = db.Game.ToList();
            
            return Json(new { data = game}, JsonRequestBehavior.AllowGet);

        }

        public JsonResult TodosEmprestimo()
        {
            var emprestismo = db.Emprestismo.Select(e=> new {e.Game,e.PessoaSolicitada,e.PessoaSolicitante }).ToList();

            return Json(new { data = emprestismo }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}