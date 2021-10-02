﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySchedulerWeb.Data;
using MySchedulerWeb.Models;

namespace MySchedulerWeb.Controllers
{
    public class DayEventsController : Controller
    {
        private readonly MySchedulerWebContext _context;

        public DayEventsController(MySchedulerWebContext context)
        {
            _context = context;
        }

        // GET: DayEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.DayEvent.ToListAsync());
        }

        // GET: DayEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayEvent = await _context.DayEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayEvent == null)
            {
                return NotFound();
            }

            return View(dayEvent);
        }

        // GET: DayEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DayEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,StartEventDate,EndEventDate,Description")] DayEvent dayEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dayEvent);
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

            var dayEvent = await _context.DayEvent.FindAsync(id);
            if (dayEvent == null)
            {
                return NotFound();
            }
            return View(dayEvent);
        }

        // POST: DayEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartEventDate,EndEventDate,EventName,Description")] DayEvent dayEvent)
        {
            if (id != dayEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dayEvent);
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

        // GET: DayEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayEvent = await _context.DayEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dayEvent == null)
            {
                return NotFound();
            }

            return View(dayEvent);
        }

        // POST: DayEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayEvent = await _context.DayEvent.FindAsync(id);
            _context.DayEvent.Remove(dayEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayEventExists(int id)
        {
            return _context.DayEvent.Any(e => e.Id == id);
        }
    }
}