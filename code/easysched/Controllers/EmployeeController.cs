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
    public class EmployeeController : Controller
    {
        private readonly easyschedContext _context;

        public EmployeeController(easyschedContext context)
        {
            _context = context;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            var easyschedContext = _context.Employee.Include(e => e.Access).Include(e => e.PhoneNumber);
            return View(await easyschedContext.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Access)
                .Include(e => e.PhoneNumber)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            ViewData["PhoneType"] = new SelectList(_context.Phonetype, "Id", "Type");
            ViewData["AccessId"] = new SelectList(_context.Access, "Id", "Rights");
            ViewData["PhoneNumberId"] = new SelectList(_context.Phonenumber, "Id", "Id");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccessId,FirstName,LastName,Employeeid,Email,PhoneNumberId")] Employee employee, string _PhoneNumber, uint _PhonetypeId)
        {
            if (ModelState.IsValid)
            {
                Phonenumber phonenum = new Phonenumber();
                phonenum.Number = _PhoneNumber;
                phonenum.PhonetypeId = _PhonetypeId;
                _context.Add(phonenum);
                await _context.SaveChangesAsync();

                employee.PhoneNumberId = phonenum.Id;
                _context.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneType"] = new SelectList(_context.Phonetype, "Id", "Type");
            ViewData["AccessId"] = new SelectList(_context.Access, "Id", "Rights", employee.AccessId);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["AccessId"] = new SelectList(_context.Access, "Id", "Rights", employee.AccessId);
            ViewData["PhoneNumberId"] = new SelectList(_context.Phonenumber, "Id", "Id", employee.PhoneNumberId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,AccessId,FirstName,LastName,Employeeid,Email,PhoneNumberId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["AccessId"] = new SelectList(_context.Access, "Id", "Rights", employee.AccessId);
            ViewData["PhoneNumberId"] = new SelectList(_context.Phonenumber, "Id", "Id", employee.PhoneNumberId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Access)
                .Include(e => e.PhoneNumber)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(uint id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
