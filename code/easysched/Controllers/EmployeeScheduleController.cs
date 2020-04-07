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
    public class EmployeeScheduleController : Controller
    {
        private readonly easyschedContext _context;

        public EmployeeScheduleController(easyschedContext context)
        {
            _context = context;
        }

        // GET: EmployeeSchedule
        public async Task<IActionResult> Index()
        {
            //need to handle if session does not exist this correctly later
            var scheduleId = Convert.ToInt32(HttpContext.Session.GetString("scheduleId"));
            var easyschedContext = _context.EmployeeSchedule.Include(e => e.Employee)
                .Include(e => e.Schedule)
                .Where( e => e.ScheduleId == scheduleId);
            
            return View(await easyschedContext.ToListAsync());
        }

        // GET: EmployeeSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .Include(e => e.Employee)
                .Include(e => e.Schedule)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // GET: EmployeeSchedule/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "EmployeeNumber");
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id");
            return View();
        }

        // POST: EmployeeSchedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,EmployeeId,Start,End")] EmployeeSchedule employeeSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "EmployeeNumber", employeeSchedule.EmployeeId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", employeeSchedule.ScheduleId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "EmployeeNumber", employeeSchedule.EmployeeId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", employeeSchedule.ScheduleId);
            return View(employeeSchedule);
        }

        // POST: EmployeeSchedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,EmployeeId,Start,End")] EmployeeSchedule employeeSchedule)
        {
            if (id != employeeSchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeScheduleExists(employeeSchedule.ScheduleId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "EmployeeNumber", employeeSchedule.EmployeeId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", employeeSchedule.ScheduleId);
            return View(employeeSchedule);
        }

        // GET: EmployeeSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeSchedule = await _context.EmployeeSchedule
                .Include(e => e.Employee)
                .Include(e => e.Schedule)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (employeeSchedule == null)
            {
                return NotFound();
            }

            return View(employeeSchedule);
        }

        // POST: EmployeeSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeSchedule = await _context.EmployeeSchedule.FindAsync(id);
            _context.EmployeeSchedule.Remove(employeeSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeScheduleExists(int id)
        {
            return _context.EmployeeSchedule.Any(e => e.ScheduleId == id);
        }
    }
}
