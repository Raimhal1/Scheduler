using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySchedulerWeb.Data;
using MySchedulerWeb.Models;

namespace MySchedulerWeb.Controllers
{
    [Authorize]
    public class DayEventsController : Controller
    {
        private readonly MySchedulerWebContext _context;

        public DayEventsController(MySchedulerWebContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DayEvents.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayEvent = await _context.DayEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayEvent == null)
            {
                return NotFound();
            }

            return View(dayEvent);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,StartEventDate,EndEventDate,Description")] DayEvent dayEvent)
        {
            if (ModelState.IsValid)
            {
                _context.DayEvents.Add(dayEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dayEvent);
        }

        // GET: DayEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dayEvent = await _context.DayEvents.FindAsync(id);
            if (dayEvent == null)
            {

                return NotFound();
            }
            return View(dayEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,StartDate,EndEventDate,Description")] DayEvent dayEvent)
        {
            if (id != dayEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.DayEvents.Update(dayEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayEventExists(dayEvent.Id))
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
            return View(dayEvent);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayEvent = await _context.DayEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayEvent == null)
            {
                return NotFound();
            }

            return View(dayEvent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayEvent = await _context.DayEvents.FindAsync(id);
            _context.DayEvents.Remove(dayEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayEventExists(int id)
        {
            return _context.DayEvents.Any(e => e.Id == id);
        }

    }
}
