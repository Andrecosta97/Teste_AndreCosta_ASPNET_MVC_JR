using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Teste_AndreCosta_ASPNET_MVC_JR.Models;

namespace Teste_AndreCosta_ASPNET_MVC_JR.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<Cliente>("ClienteViewAll"));
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            Cliente cliente;
            
            if (id == 0)
            {
                cliente = new Cliente();                
                cliente.Produtos = ListarProdutos(false);
                return View(cliente);
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                cliente = DapperORM.ReturnList<Cliente>("ClienteViewByID", param).FirstOrDefault<Cliente>();                
                cliente.Produtos = ListarProdutos(true);
                cliente.Produtos.Add(cliente.Produto);
                return View(cliente);
            }            
        }

        [HttpPost]
        public ActionResult AddOrEdit(Cliente c)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", c.Id);
            param.Add("@Nome", c.Nome);
            param.Add("@Email", c.Email);
            param.Add("@CPF", c.CPF);
            param.Add("@ProdutoId", c.ProdutoId);
            param.Add("@Produto", c.Produto);

            var check = DapperORM.ExcecuteQuery("ClienteAddorEdit", param);

            if (check)
            {                
                ViewBag.Check = true;                
                Cliente cliente = new Cliente
                {
                    Produtos = ListarProdutos(false)
                };

                return View(cliente);
            }
            else
            {                
                return RedirectToAction("Index");
            }            
        }

        public List<string> ListarProdutos(bool edit)
        {
            List<string> produtos = new List<string>();
            var teste = DapperORM.ReturnList<Produto>("ProdutoViewRelation");
            if (teste.Count() == 0 && !edit)
            {                
                Response.Redirect("../Produto/AddOrEdit?erro=true");
            }
            else
            {
                foreach (Produto p in teste)
                {
                    produtos.Add(p.Nome);
                }
            }
            return produtos;
        }

        public ActionResult Delete(int id, int produtoId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            param.Add("@ProdutoId", produtoId);
            DapperORM.ExcecuteQuery("ClienteDeleteByID", param);
            return RedirectToAction("Index");
        }

    }
}
