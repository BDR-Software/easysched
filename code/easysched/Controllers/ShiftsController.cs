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
    public class ShiftsController : Controller
    {
        private readonly easyschedContext _context;

        public ShiftsController(easyschedContext context)
        {
            _context = context;
        }

        // GET: Shifts
        public async Task<IActionResult> Index()
        {
            var easyschedContext = _context.Shift.Include(s => s.Employee).Include(s => s.Schedule);
            Employee employee = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
            ViewData["EmployeeName"] = employee.FirstName + " " + employee.LastName;
            return View(await easyschedContext.ToListAsync());
        }

        [Route("shifts/findall")]
        public IActionResult FindAllShifts()
        {
            Employee employee = _context.Employee.FirstOrDefault(e => e.Id == HttpContext.Session.GetInt32("LoggedInEmployeeID"));
            var shifts = _context.Shift.Where(a => a.EmployeeId == employee.Id)
                                       .Select(s => new
                                       {
                                           id = s.Id,
                                           title = s.Start.Value.ToString("HH:mm") + " - " + s.End.Value.ToString("HH:mm"),
                                           start = s.Start.Value.ToString("MM/dd/yyyy"),
                                           end = s.End.Value.ToString("MM/dd/yyyy"),
                                           backgroundColor = "#99c0ff"
                                       }).ToList();
            return new JsonResult(shifts);
        }

        // GET: Shifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .Include(s => s.Employee)
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id");
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id");
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,ScheduleId,Start,End")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", shift.EmployeeId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", shift.ScheduleId);
            return View(shift);
        }

        // GET: Shifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", shift.EmployeeId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", shift.ScheduleId);
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,ScheduleId,Start,End")] Shift shift)
        {
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", shift.EmployeeId);
            ViewData["ScheduleId"] = new SelectList(_context.Schedule, "Id", "Id", shift.ScheduleId);
            return View(shift);
        }

        // GET: Shifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .Include(s => s.Employee)
                .Include(s => s.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift = await _context.Shift.FindAsync(id);
            _context.Shift.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shift.Any(e => e.Id == id);
        }
    }
}
