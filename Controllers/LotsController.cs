﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BidDecore.Database;
using BidDecore.Models;

namespace BidDecore.Controllers
{
    public class LotsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public LotsController(ApplicationDbContext context)
        {
            _context = context;

        }


        // GET: Lots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lot.Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lot
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // GET: Lots/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Lots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LotId,UserId,StartPrice,BuyOutPrice,Description,Deadline,Category,ImageData")] Lot lot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            _context.Add(lot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", lot.UserId);
            return View(lot);
        }

        // GET: Lots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lot.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", lot.UserId);
            return View(lot);
        }

        // POST: Lots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LotId,UserId,StartPrice,BuyOutPrice,Description,Deadline,Category,ImageData")] Lot lot)
        {
            if (id != lot.LotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.LotId))
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
            //
            _context.Update(lot);
            await _context.SaveChangesAsync();
            //
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", lot.UserId);
            return View(lot);
        }

        // GET: Lots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lot
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // POST: Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lot = await _context.Lot.FindAsync(id);
            if (lot != null)
            {
                _context.Lot.Remove(lot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(int id)
        {
            return _context.Lot.Any(e => e.LotId == id);
        }
    }
}
