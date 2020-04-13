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
    public class LoginsController : Controller
    {
        private readonly easyschedContext _context;

        public LoginsController(easyschedContext context)
        {
            _context = context;
        }

        [Route("login")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<IActionResult> Index([Bind("Email,Pass,ConfirmPass")] Login login)
        {
            if (ModelState.IsValid)
            {
               try
                {
                    var loginFound = await _context.Login.FirstOrDefaultAsync(m => m.Email.ToLower() == login.Email.ToLower() && m.Pass == login.Pass);
                    if (loginFound != null)
                    {
                        HttpContext.Session.SetInt32("UserLoggedIn", 1);
                    }

                    if (loginFound.EmployeeId != null)
                    {
                        HttpContext.Session.SetInt32("LoggedInEmployeeID", (int)loginFound.EmployeeId);
                        var employeeFound = await _context.Employee.Include(e => e.Priveleges)
                                                                   .FirstOrDefaultAsync(e => e.Id == (int)loginFound.EmployeeId);
                        HttpContext.Session.SetInt32("UserPriveleges", (int)employeeFound.Priveleges.Type);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //show page saying account hasn't been associated with an employee yet   
                    }
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.InnerException.Message.ToString();
                }
                
            }
            return View(login);
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("logout")]
        public async Task<IActionResult> Logout([Bind("Email,Pass")] Login login)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.Remove("UserLoggedIn");
                HttpContext.Session.Remove("UserPriveleges");
                HttpContext.Session.Remove("LoggedInEmployeeID");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        [Route("register")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<IActionResult> Create([Bind("Id,Email,Pass,ConfirmPass,EmployeeId")] Login login)
        {
            Login foundLogin = _context.Login.FirstOrDefault(l => l.EmployeeId == login.EmployeeId);

            if (ModelState.IsValid && foundLogin == null)
            {
                if (foundLogin == null)
                {
                    _context.Add(login);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetInt32("UserLoggedIn", 1);

                    var employeeWithEmail = await _context.Employee.FirstOrDefaultAsync(e => e.Email == login.Email);
                    if (employeeWithEmail == null)
                    {
                        //show page saying account hasn't been associated with an employee yet
                        return RedirectToAction("Index", "RegistrationUnfinished");
                    }
                    else
                    {
                        login.Email = employeeWithEmail.Email;
                        _context.Update(login);
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Email has already been registered.";
                }
                

            }            
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", login.EmployeeId);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Pass,EmployeeId")] Login login)
        {
            if (id != login.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", login.EmployeeId);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Login.FindAsync(id);
            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.Id == id);
        }
    }
}
