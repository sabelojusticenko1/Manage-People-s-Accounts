using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagePeople.ManangeDbContext;
using ManagePeople.Models;
using Microsoft.AspNetCore.Authorization;

namespace ManagePeople.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ManageDbContext _context;

     
        public PeopleController(ManageDbContext context)
        {
            _context = context;
        }
        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbPerson.ToListAsync());
        }

        //GET: Search people
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(string SearchPeople)
        {
            ViewData["GetSearchPeople"] = SearchPeople;

            var peoplequery = from x in _context.TbPerson select x;
            if (!String.IsNullOrEmpty(SearchPeople)) 
            {
                peoplequery = peoplequery.Where(x => x.Surname.Contains(SearchPeople) || x.IDNumber.Contains(SearchPeople));
            }

            return View(await peoplequery.AsNoTracking().ToListAsync());
        }
        // GET: People/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.TbPerson
                .FirstOrDefaultAsync(m => m.IdPerson == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPerson,FirstName,Surname,Age,IDNumber,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.TbPerson.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPerson,FirstName,Surname,Age,IDNumber,Address")] Person person)
        {
            if (id != person.IdPerson)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.IdPerson))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.TbPerson
                .FirstOrDefaultAsync(m => m.IdPerson == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.TbPerson.FindAsync(id);
            _context.TbPerson.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.TbPerson.Any(e => e.IdPerson == id);
        }
    }
}
