using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return View(await _context.DayEvents.Include(e => e.Users)
                .ThenInclude(u => u.DayEvents)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayEvent = await _context.DayEvents.Include(e => e.Users)
                .ThenInclude(u => u.DayEvents)
                .FirstOrDefaultAsync(ev => ev.Id == id);

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
        public async Task<IActionResult> Create(DayEvent dayEvent)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    dayEvent.Users.Add(user);
                    dayEvent.Creator = user.Email;

                    user.DayEvents.Add(dayEvent);

                    _context.DayEvents.Add(dayEvent);
                    _context.Users.Update(user);

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dayEvent);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dayEvent = await _context.DayEvents.Include(e => e.Users)
                .ThenInclude(u => u.DayEvents)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dayEvent == null)
            {

                return NotFound();
            }
            return View(dayEvent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DayEvent dayEvent)
        {
            if (id != dayEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
                if (user != null)
                {
                    try
                    {
                        dayEvent.Creator = user.Email;
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

            var dayEvent = await _context.DayEvents.Include(e => e.Users)
                .ThenInclude(u => u.DayEvents)
                .FirstOrDefaultAsync(d => d.Id == id);

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
            var users = await _context.Users.Include(u => u.DayEvents)
                .Where(e => e.Id == dayEvent.Id)
                .ToListAsync();

            foreach(var user in users)
            {
                user.DayEvents.Remove(dayEvent);
                _context.Users.Update(user);
                foreach(var e in user.DayEvents)
                {
                    Debug.WriteLine(e.EventName);
                }
            }
            _context.DayEvents.Remove(dayEvent);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayEventExists(int id)
        {
            return _context.DayEvents.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SignInOut(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayEvent = await _context.DayEvents.Include(e => e.Users)
                .ThenInclude(u => u.DayEvents)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (dayEvent == null)
            {
                return NotFound();
            }

            return View(dayEvent);
        }

        [HttpPost, ActionName("SignInOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInConfirmed(int? id)
        {
            var dayEvent = await _context.DayEvents.Include(e => e.Users)
                .ThenInclude(u => u.DayEvents)
                .FirstOrDefaultAsync(d => d.Id == id);

            var user = await _context.Users.Include(u => u.DayEvents)
                .ThenInclude(e => e.Users)
                .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            if (!dayEvent.Users.Contains(user))
            {
                user.DayEvents.Add(dayEvent);
                dayEvent.Users.Add(user);
            }
            else
            {
                user.DayEvents.Remove(dayEvent);
                dayEvent.Users.Remove(user);
            }

            _context.DayEvents.Update(dayEvent);
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));    
        }

    }
}
