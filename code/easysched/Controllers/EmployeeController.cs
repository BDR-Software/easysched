using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easysched.Data;
using easysched.Models;
using Microsoft.AspNetCore.Http;

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
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    var employeeLoggedIn = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
                    var easyschedContext = _context.Employee.Include(e => e.Company).Include(e => e.Priveleges).Where(e => e.CompanyId == employeeLoggedIn.CompanyId);
                    return View(await easyschedContext.ToListAsync());
                }
                else
                {
                    return RedirectToAction("Index", "Shifts");
                }
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
            
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }
                    var employee = await _context.Employee
                                    .Include(e => e.Company)
                                    .Include(e => e.Priveleges)
                                    .FirstOrDefaultAsync(m => m.Id == id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Index", "Shifts");
                }
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }  

            
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    ViewData["PrivelegesId"] = new SelectList(_context.Priveleges, "Id", "Name");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Shifts");
                }
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
            
        }

        // POST: Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrivelegesId,CompanyId,FirstName,LastName,EmployeeNumber,Email,Wages")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var employeeFound = await _context.Employee.Include(e => e.Priveleges)
                                                               .FirstOrDefaultAsync(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
                employee.CompanyId = employeeFound.CompanyId;
                _context.Add(employee);
                await _context.SaveChangesAsync();

                var loginFound = await _context.Login.FirstOrDefaultAsync(l => l.Email == employee.Email);
                if (loginFound != null)
                {
                    loginFound.EmployeeId = employee.Id;
                    _context.Update(loginFound);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction(nameof(Index));
            }
            ViewData["PrivelegesId"] = new SelectList(_context.Priveleges, "Id", "Name", employee.Priveleges);
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    var employee = await _context.Employee.FindAsync(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", employee.Company);
                    ViewData["PrivelegesId"] = new SelectList(_context.Priveleges, "Id", "Name", employee.Priveleges);
                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Index", "Shifts");
                }
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
            if (id == null)
            {
                return NotFound();
            }

            
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrivelegesId,CompanyId,FirstName,LastName,EmployeeNumber,Email,Wages")] Employee employee)
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", employee.CompanyId);
            ViewData["PrivelegesId"] = new SelectList(_context.Priveleges, "Id", "Name", employee.PrivelegesId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var employee = await _context.Employee
                        .Include(e => e.Company)
                        .Include(e => e.Priveleges)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (employee == null)
                    {
                        return NotFound();
                    }

                    return View(employee);
                }
                else
                {
                    return RedirectToAction("Index", "Shifts");
                }
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
            
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
