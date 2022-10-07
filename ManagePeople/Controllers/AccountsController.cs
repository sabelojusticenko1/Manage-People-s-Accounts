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
    public class AccountsController : Controller
    {
        private readonly ManageDbContext _context;

        public AccountsController(ManageDbContext context)
        {
            _context = context;
        }



        // GET: Accounts
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.TbAccounts
                .Include(a => a.IdPersonNoNavigation)
                .Where(m => m.IdPerson == id).ToListAsync();
            if (accounts == null)
            {
                return NotFound();
            }
            TempData["IdPerson"] = id;
            return View(accounts);
        }

        // GET: Accounts/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.TbAccounts
                .Include(a => a.IdPersonNoNavigation)
                .FirstOrDefaultAsync(m => m.IdAccount == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAccount,IdPerson,AccountNo,AccountHolder,AccountName")] Accounts accounts)
        {
            accounts.IdPerson = Convert.ToInt32(TempData["IdPerson"]);
            if (ModelState.IsValid)
            {
                //var StatusList = _context.TbAccountStatus.ToList();
                //ViewBag.IdStatus = new SelectList(StatusList, "IdStatus", "Status");

                _context.Add(accounts);
                await _context.SaveChangesAsync();
                return Redirect("/Accounts/Index/"+ accounts.IdPerson);
            }
            TempData["IdPerson"] = accounts.IdPerson;
            return View(accounts);
        }

        // GET: Accounts/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.TbAccounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }
            ViewData["IdPerson"] = new SelectList(_context.TbPerson, "IdPerson", "IdPerson", accounts.IdPerson);
            return View(accounts);
        }

        // POST: Accounts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAccount,IdPerson,AccountNo,AccountHolder,AccountName")] Accounts accounts)
        {
            if (id != accounts.IdAccount)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountsExists(accounts.IdAccount))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Accounts/Index/" + accounts.IdPerson);
                //return RedirectToAction(nameof(Index));
            }
            ViewData["IdPerson"] = new SelectList(_context.TbPerson, "IdPerson", "IdPerson", accounts.IdPerson);
           
            return View(accounts);
        }

        // GET: Accounts/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.TbAccounts
                .Include(a => a.IdPersonNoNavigation)
                .FirstOrDefaultAsync(m => m.IdAccount == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // POST: Accounts/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accounts = await _context.TbAccounts.FindAsync(id);
            _context.TbAccounts.Remove(accounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountsExists(int id)
        {
            return _context.TbAccounts.Any(e => e.IdAccount == id);
        }
    }
}
