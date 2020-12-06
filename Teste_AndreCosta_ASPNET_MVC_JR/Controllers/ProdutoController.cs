using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Teste_AndreCosta_ASPNET_MVC_JR.Models;

namespace Teste_AndreCosta_ASPNET_MVC_JR.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<Produto>("ProdutoViewAll"));
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DapperORM.ReturnList<Produto>("ProdutoViewByID", param).FirstOrDefault<Produto>());
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(Produto p)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", p.Id);
            param.Add("@Nome", p.Nome);
            param.Add("@Ativo", p.Ativo);

            var check = DapperORM.ExcecuteQuery("ProdutoAddorEdit", param);

            if (check)
            {
                ViewBag.Check = true;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Delete(int id, bool bit)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@Bit", bit);
            DapperORM.ExcecuteQuery("ProdutoDeleteByID", param);
            return RedirectToAction("Index");
        }

        public ActionResult ItemAtivo()
        {
            return null;
        }
    }
}
