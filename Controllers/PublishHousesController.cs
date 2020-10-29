using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KsiegarniaOnline.DAL;
using KsiegarniaOnline.Models;

namespace KsiegarniaOnline.Controllers
{
    public class PublishHousesController : Controller
    {
        private readonly KsiegarniaOnlineContext _context;

        public PublishHousesController(KsiegarniaOnlineContext context)
        {
            _context = context;
        }

        // GET: PublishHouses
        public async Task<IActionResult> Index()
        {
            return View(await _context.PublishHouses.ToListAsync());
        }

        // GET: PublishHouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishHouse = await _context.PublishHouses
                .FirstOrDefaultAsync(m => m.IdPublishHouse == id);
            if (publishHouse == null)
            {
                return NotFound();
            }

            return View(publishHouse);
        }

        // GET: PublishHouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublishHouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPublishHouse,PublishHouseName,PublishmentYear")] PublishHouse publishHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publishHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publishHouse);
        }

        // GET: PublishHouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishHouse = await _context.PublishHouses.FindAsync(id);
            if (publishHouse == null)
            {
                return NotFound();
            }
            return View(publishHouse);
        }

        // POST: PublishHouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPublishHouse,PublishHouseName,PublishmentYear")] PublishHouse publishHouse)
        {
            if (id != publishHouse.IdPublishHouse)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publishHouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublishHouseExists(publishHouse.IdPublishHouse))
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
            return View(publishHouse);
        }

        // GET: PublishHouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publishHouse = await _context.PublishHouses
                .FirstOrDefaultAsync(m => m.IdPublishHouse == id);
            if (publishHouse == null)
            {
                return NotFound();
            }

            return View(publishHouse);
        }

        // POST: PublishHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publishHouse = await _context.PublishHouses.FindAsync(id);
            _context.PublishHouses.Remove(publishHouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublishHouseExists(int id)
        {
            return _context.PublishHouses.Any(e => e.IdPublishHouse == id);
        }
    }
}
