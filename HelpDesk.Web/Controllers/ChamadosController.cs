using System.Drawing;
using HelpDesk.Web.Data;
using HelpDesk.Web.Models;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace HelpDesk.Web.Controllers
{
    public class ChamadosController : Controller
    {
        private readonly AppDbContext _context;
        public ChamadosController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var chamados = await _context.Chamados
                .Include(c => c.Usuario)
                .OrderByDescending (c => c.Criado)
                .ToListAsync();

            return View(chamados);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chamado chamado)
        {
            if (ModelState.IsValid)
            {
                chamado.UsuarioId = 1;
                chamado.Status = "Aberto";
                chamado.Criado = DateTime.Now;

                _context.Chamados.Add(chamado);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(chamado);
        }
        public async Task<IActionResult> Details (int id)
        {
            var chamado = await _context.Chamados
                .Include(c => c.Usuario)
                .Include(c => c.Comentarios)
                .ThenInclude(comentario => comentario.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (chamado == null)
            {
                return NotFound();
            }
            return View(chamado);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null)
            {
                return NotFound();
            }
            return View(chamado);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Chamado chamado)
        {
            if (id != chamado.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                chamado.AtualizadoEm = DateTime.Now;
                _context.Update(chamado);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(chamado);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var chamado = await _context.Chamados
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (chamado == null)
            {
                return NotFound();
            }
            return View(chamado);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado != null)
            {
                _context.Chamados.Remove(chamado);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarComentario(int chamadoId, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(mensagem))
            {
                return RedirectToAction(nameof(Details), new { id = chamadoId});
            }
            var chamado = await _context.Chamados.FindAsync(chamadoId);
            if (chamado == null)
            {
                return NotFound();
            }
            var comentario = new ComentarioChamado
            {
                ChamadoId = chamadoId,
                UsuarioId = 1,
                Mensagem = mensagem,
                CriadoEm = DateTime.Now
            };

            _context.ComentariosChamado.Add(comentario);
            chamado.AtualizadoEm = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = chamadoId});
        }
    }
}