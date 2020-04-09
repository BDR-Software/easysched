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
    public class ScheduleController : Controller
    {
        private readonly easyschedContext _context;

        public ScheduleController(easyschedContext context)
        {
            _context = context;
        }

        // GET: Schedule
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    var easyschedContext = _context.Schedule.Include(s => s.Department);
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

        // GET: Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    var schedule = await _context.Schedule
                        .Include(s => s.Department)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (schedule == null)
                    {
                        return NotFound();
                    }

                    return View(schedule);
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

        // GET: Schedule/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
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

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Week,DepartmentId")] Schedule schedule)
        {
            var scheduleExists = _context.Schedule.Any(s => s.Week == schedule.Week && s.DepartmentId == schedule.DepartmentId);
            if (!scheduleExists)
            {   
                if (ModelState.IsValid)
                {
                    _context.Add(schedule);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                schedule = _context.Schedule.FirstOrDefault(s => s.Week == schedule.Week && s.DepartmentId == schedule.DepartmentId);
            }
            HttpContext.Session.SetString("scheduleId", schedule.Id.ToString());
            return RedirectToAction("index", "EmployeeSchedule");
       
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("UserLoggedIn") == 1)
            {
                if (HttpContext.Session.GetInt32("UserPriveleges") == 2)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var schedule = await _context.Schedule.FindAsync(id);
                    if (schedule == null)
                    {
                        return NotFound();
                    }
                    ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", schedule.DepartmentId);
                    return View(schedule);
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

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Week,DepartmentId")] Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Id", schedule.DepartmentId);
            return View(schedule);
        }

        // GET: Schedule/Delete/5
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

                    var schedule = await _context.Schedule
                        .Include(s => s.Department)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (schedule == null)
                    {
                        return NotFound();
                    }

                    return View(schedule);
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

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedule.Any(e => e.Id == id);
        }
    }
}
