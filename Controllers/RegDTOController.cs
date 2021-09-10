using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoAuthorityProject.Data;
using DemoAuthorityProject.Models;

namespace DemoAuthorityProject.Controllers
{
    public class RegDTOController : Controller
    {
        private readonly RegContext _context;

        public RegDTOController(RegContext context)
        {
            _context = context;
        }

        // GET: RegDTO
        public async Task<IActionResult> Index()
        {
            return View(await _context.iProducts.ToListAsync());
        }

        // GET: RegDTO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regDTO = await _context.iProducts
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (regDTO == null)
            {
                return NotFound();
            }

            return View(regDTO);
        }

        // GET: RegDTO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegDTO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,FirstName,LastName,Bithdate,Status")] RegDTO regDTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regDTO);
        }

        // GET: RegDTO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regDTO = await _context.iProducts.FindAsync(id);
            if (regDTO == null)
            {
                return NotFound();
            }
            return View(regDTO);
        }

        // POST: RegDTO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,FirstName,LastName,Bithdate,Status")] RegDTO regDTO)
        {
            if (id != regDTO.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegDTOExists(regDTO.UserID))
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
            return View(regDTO);
        }

        // GET: RegDTO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regDTO = await _context.iProducts
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (regDTO == null)
            {
                return NotFound();
            }

            return View(regDTO);
        }

        // POST: RegDTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regDTO = await _context.iProducts.FindAsync(id);
            _context.iProducts.Remove(regDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegDTOExists(int id)
        {
            return _context.iProducts.Any(e => e.UserID == id);
        }
    }
}
