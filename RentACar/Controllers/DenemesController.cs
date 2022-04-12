using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Models;

namespace RentACar.Controllers
{
    public class DenemesController : Controller
    {
        private readonly RentACarDBContext _context;

        public DenemesController(RentACarDBContext context)
        {
            _context = context;
        }

        // GET: Denemes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Denemeler.ToListAsync());
        }

        // GET: Denemes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deneme = await _context.Denemeler
                .FirstOrDefaultAsync(m => m.DenemeId == id);
            if (deneme == null)
            {
                return NotFound();
            }

            return View(deneme);
        }

        // GET: Denemes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Denemes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DenemeId,DenemeName")] Deneme deneme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deneme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deneme);
        }

        // GET: Denemes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deneme = await _context.Denemeler.FindAsync(id);
            if (deneme == null)
            {
                return NotFound();
            }
            return View(deneme);
        }

        // POST: Denemes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DenemeId,DenemeName")] Deneme deneme)
        {
            if (id != deneme.DenemeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deneme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DenemeExists(deneme.DenemeId))
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
            return View(deneme);
        }

        // GET: Denemes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deneme = await _context.Denemeler
                .FirstOrDefaultAsync(m => m.DenemeId == id);
            if (deneme == null)
            {
                return NotFound();
            }

            return View(deneme);
        }

        // POST: Denemes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deneme = await _context.Denemeler.FindAsync(id);
            _context.Denemeler.Remove(deneme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DenemeExists(int id)
        {
            return _context.Denemeler.Any(e => e.DenemeId == id);
        }
    }
}
