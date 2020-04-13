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
    public class TimeOffRequestsController : Controller
    {
        private readonly easyschedContext _context;

        public TimeOffRequestsController(easyschedContext context)
        {
            _context = context;
        }

        // GET: TimeOffRequests
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                var currentEmployee = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    var easyschedContext = _context.Timeoffrequest.Include(t => t.Employee).Where(s => s.Employee.CompanyId == currentEmployee.CompanyId);
                    return View(await easyschedContext.ToListAsync());
                }
                else
                {
                    var easyschedContext = _context.Timeoffrequest.Include(t => t.Employee).Where(s => s.EmployeeId == currentEmployee.Id);
                    return View(await easyschedContext.ToListAsync());
                }
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
            
        }

        // GET: TimeOffRequests/ChangeStatus/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeoffrequest = await _context.Timeoffrequest
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeoffrequest == null)
            {
                return NotFound();
            }

            return View(timeoffrequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, string submit, [Bind("Id,EmployeeId,Created,Day,Message,Approved")] Timeoffrequest timeoffrequest)
        {
            if (id != timeoffrequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    switch(submit)
                    {
                        case "Approve":
                            timeoffrequest.Approved = true;
                            break;
                        case "Deny":
                            timeoffrequest.Approved = false;
                            break;
                    }
                    _context.Update(timeoffrequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeoffrequestExists(timeoffrequest.Id))
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
            return View(timeoffrequest);
        }

        // GET: TimeOffRequests/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = HttpContext.Session.GetInt32("LoggedInEmployeeID");
            return View();
        }

        // POST: TimeOffRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Created,Day,Message,Approved")] Timeoffrequest timeoffrequest)
        {
            timeoffrequest.Created = DateTime.Now;
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(timeoffrequest);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(timeoffrequest);
            }
            else
            {
                return RedirectToAction("Index", "Logins");
            }
            
        }

        // GET: TimeOffRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeoffrequest = await _context.Timeoffrequest.FindAsync(id);
            if (timeoffrequest == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", timeoffrequest.EmployeeId);
            return View(timeoffrequest);
        }

        // POST: TimeOffRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Created,Day,Message,Approved")] Timeoffrequest timeoffrequest)
        {
            if (id != timeoffrequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeoffrequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeoffrequestExists(timeoffrequest.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FullName", timeoffrequest.EmployeeId);
            return View(timeoffrequest);
        }

        // GET: TimeOffRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeoffrequest = await _context.Timeoffrequest
                .Include(t => t.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeoffrequest == null)
            {
                return NotFound();
            }

            return View(timeoffrequest);
        }

        // POST: TimeOffRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeoffrequest = await _context.Timeoffrequest.FindAsync(id);
            _context.Timeoffrequest.Remove(timeoffrequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeoffrequestExists(int id)
        {
            return _context.Timeoffrequest.Any(e => e.Id == id);
        }
    }
}
