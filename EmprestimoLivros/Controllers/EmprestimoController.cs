using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _context;
        public EmprestimoController(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModel> emprestimos = _context.Emprestimos;
            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimos)
        {
            if (ModelState.IsValid) 
            { 
                _context.Emprestimos.Add(emprestimos);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                return RedirectToAction("Index");   
                           
            }
            return View();

        }
        [HttpGet]
        public IActionResult Editar(int? id)
        { 
            if (id == null || id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimo = _context.Emprestimos.FirstOrDefault(x => x.Id == id);

            if (emprestimo == null) 
            {
                return NotFound();

            }

            return View(emprestimo);
        
        }
        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimo)
        {
            if(ModelState.IsValid) 
            { 
                _context.Emprestimos.Update(emprestimo);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Edição realizado com sucesso!";

                return RedirectToAction("Index");
            
            }
            TempData["MensagemErro"] = "Ocorreu erro ao realizar a edição!";
            return View(emprestimo);
        }

        [HttpGet]
        public IActionResult Excluir(int? id) 
        { 
            if (id == null || id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimo = _context.Emprestimos.FirstOrDefault(x => x.Id == id);

            if(emprestimo == null)
            {
                return NotFound();
            }
            return View(emprestimo);
        }
        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimos)
        {
            if (emprestimos == null)
            {
                return NotFound();
            }

            _context.Emprestimos.Remove(emprestimos);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Cadastro excluido com sucesso!";

            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id)
        {

            if(id == null || id == 0)
            {
                return NotFound();
            }
            var emprestimo = _context.Emprestimos.FirstOrDefault(e => e.Id ==id);
            if(emprestimo == null)
            {
                return NotFound(emprestimo);
            }

            return View(emprestimo);
        }
    }
}
