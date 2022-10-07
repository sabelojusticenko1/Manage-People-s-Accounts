using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagePeople.ManangeDbContext;
using ManagePeople.Models;

namespace ManagePeople.Controllers
{
    public class TransationsController : Controller
    {
        private readonly ManageDbContext _context;

        public TransationsController(ManageDbContext context)
        {
            _context = context;
        }

        // GET: Transations
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transations = await _context.TbTransations
                .Include(a => a.IdAccountsNoNavigation)
                .Where(m => m.IdAccount == id).ToListAsync();
            if (transations == null)
            {
                return NotFound();
            }
            TempData["IdAccount"] = id;
            return View(transations);
        }

        // GET: Transations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transations = await _context.TbTransations
                .FirstOrDefaultAsync(m => m.TransationId == id);
            if (transations == null)
            {
                return NotFound();
            }

            return View(transations);
        }

        // GET: Transations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransationId,TransationDate,AccountHolder,TransationDetails")] Transations transations)
        {
            transations.IdAccount = Convert.ToInt32(TempData["IdAccount"]);
            if (ModelState.IsValid)
            {
                _context.Add(transations);
                await _context.SaveChangesAsync();
                return Redirect("/Transations/Index/" + transations.IdAccount);
            }
            TempData["IdAccount"] = transations.IdAccount;
            return View(transations);
        }

        // GET: Transations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transations = await _context.TbTransations.FindAsync(id);
            if (transations == null)
            {
                return NotFound();
            }
            
            return View(transations);
        }

        // POST: Transations/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransationId,TransationDate,AccountHolder,TransationDetails")] Transations transations)
        {
            transations.IdAccount = Convert.ToInt32(TempData["IdAccount"]);
            if (id != transations.TransationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransationsExists(transations.TransationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Transations/Index/" + transations.IdAccount);
                //return RedirectToAction(nameof(Index));
            }
            TempData["IdAccount"] = transations.IdAccount;
            return View(transations);
        }

        // GET: Transations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transations = await _context.TbTransations
                .FirstOrDefaultAsync(m => m.TransationId == id);
            if (transations == null)
            {
                return NotFound();
            }

            return View(transations);
        }

        // POST: Transations/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transations = await _context.TbTransations.FindAsync(id);
            _context.TbTransations.Remove(transations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransationsExists(int id)
        {
            return _context.TbTransations.Any(e => e.TransationId == id);
        }
    }
}
