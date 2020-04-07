using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easysched.Data;
using easysched.Models;

namespace easysched.Controllers
{
    public class PhonenumberController : Controller
    {
        private readonly easyschedContext _context;

        public PhonenumberController(easyschedContext context)
        {
            _context = context;
        }

        // GET: Phonenumber
        public async Task<IActionResult> Index()
        {
            var easyschedContext = _context.Phonenumber.Include(p => p.Phonetype);
            return View(await easyschedContext.ToListAsync());
        }

        // GET: Phonenumber/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phonenumber = await _context.Phonenumber
                .Include(p => p.Phonetype)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phonenumber == null)
            {
                return NotFound();
            }

            return View(phonenumber);
        }

        // GET: Phonenumber/Create
        public IActionResult Create(uint? employeeId)
        {
            ViewData["Employee"] = _context.Employee.Where(e => e.Id == employeeId);
            ViewData["PhonetypeId"] = new SelectList(_context.Phonetype, "Id", "Id");
            return View();
        }

        // POST: Phonenumber/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PhonetypeId,Number")] Phonenumber phonenumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phonenumber);
                await _context.SaveChangesAsync();
                Employee employee = (Employee)ViewData["Employee"];
                employee.PhoneNumberId = phonenumber.Id;
                _context.Update(employee);

                return RedirectToAction("Employee", "Create");
            }
            ViewData["PhonetypeId"] = new SelectList(_context.Phonetype, "Id", "Id", phonenumber.PhonetypeId);
            return View(phonenumber);
        }

        // GET: Phonenumber/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phonenumber = await _context.Phonenumber.FindAsync(id);
            if (phonenumber == null)
            {
                return NotFound();
            }
            ViewData["PhonetypeId"] = new SelectList(_context.Phonetype, "Id", "Id", phonenumber.PhonetypeId);
            return View(phonenumber);
        }

        // POST: Phonenumber/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,PhonetypeId,Number")] Phonenumber phonenumber)
        {
            if (id != phonenumber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phonenumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhonenumberExists(phonenumber.Id))
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
            ViewData["PhonetypeId"] = new SelectList(_context.Phonetype, "Id", "Id", phonenumber.PhonetypeId);
            return View(phonenumber);
        }

        // GET: Phonenumber/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phonenumber = await _context.Phonenumber
                .Include(p => p.Phonetype)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phonenumber == null)
            {
                return NotFound();
            }

            return View(phonenumber);
        }

        // POST: Phonenumber/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var phonenumber = await _context.Phonenumber.FindAsync(id);
            _context.Phonenumber.Remove(phonenumber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhonenumberExists(uint id)
        {
            return _context.Phonenumber.Any(e => e.Id == id);
        }
    }
}
