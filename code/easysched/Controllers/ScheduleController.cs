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
                    var currentEmployee = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
                    var easyschedContext = _context.Schedule.Include(s => s.Department).Where(s => s.CompanyId == currentEmployee.CompanyId);
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

        [Route("schedule/findall")]
        public IActionResult FindAllSchedules()
        {
            Employee employee = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
            var schedules = _context.Schedule.Include(s => s.Department)
                                             .Where(s => s.CompanyId == employee.CompanyId)
                                            .Select(s => new
                                            {
                                                id = s.Id,
                                                title = s.Department.Name,
                                                start = s.Start.Value.ToString("MM/dd/yyyy"),
                                                end = s.End.Value.AddDays(1).ToString("MM/dd/yyyy"),
                                                backgroundColor = "#99c0ff"
                                            }).ToList();
            return new JsonResult(schedules);
        }

        // GET: Schedule/Details/5
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
                    var schedule = await _context.Schedule
                        .Include(s => s.Department)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (schedule == null)
                    {
                        return NotFound();
                    }

                    var shifts = _context.Shift.Include(s => s.Employee).Where(s => s.ScheduleId == id).ToList();
                    ViewData["shifts"] = shifts;

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
        public async Task<IActionResult> Create([Bind("Start,End,DepartmentId")] Schedule schedule)
        {
            var scheduleExists = _context.Schedule.Any(s => s.Start == schedule.Start && s.End == schedule.End && s.DepartmentId == schedule.DepartmentId);
            if (!scheduleExists)
            {   
                if (ModelState.IsValid)
                {
                    var employeeLoggedIn = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
                    schedule.CompanyId = employeeLoggedIn.CompanyId;
                    _context.Add(schedule);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "A schedule for that department during that week has already been created.";
                return View(schedule);
            }
            HttpContext.Session.SetInt32("scheduleId", schedule.Id);
            return RedirectToAction("Create", "Shifts");
       
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
                    ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", schedule.DepartmentId);
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", schedule.DepartmentId);
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
