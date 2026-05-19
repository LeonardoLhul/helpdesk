using HelpDesk.Web.Data;
using HelpDesk.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HelpDesk.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar (Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var emailJaExiste = await _context.Usuarios 
                    .AnyAsync(u => u.Email == usuario.Email);
                
                if (emailJaExiste)
                {
                    ModelState.AddModelError("Email", "Este e-mail já está cadastrado.");
                    return View(usuario);
                }
                usuario.Perfil = "Cliente";
                usuario.Criado = DateTime.Now;
                _context.Usuarios.Add(usuario);
                return RedirectToAction("Login", "Auth");
            }
            return View(usuario);
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
