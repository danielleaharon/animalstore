using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Store.Data;
using Animal_Store.Models;
using Microsoft.AspNetCore.Http;

namespace Animal_Store.Controllers
{
    public class wish_listController : Controller
    {
        private readonly Animal_StoreContext _context;

        public wish_listController(Animal_StoreContext context)
        {
            _context = context;
        }

        // GET: wish_list
        public async Task<IActionResult> Index()
        {
            var animal_StoreContext = _context.wish_list.Include(w => w.Customer).Include(w => w.pet);
            return View(await animal_StoreContext.ToListAsync());
        }

        // GET: wish_list/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wish_list = await _context.wish_list
                .Include(w => w.Customer)
                .Include(w => w.pet)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (wish_list == null)
            {
                return NotFound();
            }

            return View(wish_list);
        }

        // GET: wish_list/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID");
            ViewData["PetsId"] = new SelectList(_context.Pets, "ID", "ID");
            return View();
        }

        // POST: wish_list/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetsId,CustomerId")] wish_list wish_list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wish_list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID", wish_list.CustomerId);
            ViewData["PetsId"] = new SelectList(_context.Pets, "ID", "ID", wish_list.PetsId);
            return View(wish_list);
        }

        // GET: wish_list/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wish_list = await _context.wish_list.FindAsync(id);
            if (wish_list == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID", wish_list.CustomerId);
            ViewData["PetsId"] = new SelectList(_context.Pets, "ID", "ID", wish_list.PetsId);
            return View(wish_list);
        }

        // POST: wish_list/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetsId,CustomerId")] wish_list wish_list)
        {
            if (id != wish_list.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wish_list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!wish_listExists(wish_list.CustomerId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID", wish_list.CustomerId);
            ViewData["PetsId"] = new SelectList(_context.Pets, "ID", "ID", wish_list.PetsId);
            return View(wish_list);
        }

        // GET: wish_list/Delete/5
        public async Task<IActionResult> Delete(int? id, int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wish_list = await _context.wish_list
                .Include(w => w.Customer)
                .Include(w => w.pet)
                .FirstOrDefaultAsync(m => m.CustomerId == id&& m.PetsId == id2);
            if (wish_list == null)
            {
                return NotFound();
            }

            return View(wish_list);
        }

        // POST: wish_list/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int PetsId, int CustomerId)
        {

          //  var wish_list = await _context.wish_list.FindAsync(PetsId, CustomerId);
            var wish_list = await _context.wish_list
               .Include(w => w.Customer)
               .Include(w => w.pet)
               .FirstOrDefaultAsync(m => m.CustomerId == CustomerId && m.PetsId == PetsId);
            _context.wish_list.Remove(wish_list);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Pets");
        }

        private bool wish_listExists(int id)
        {
            return _context.wish_list.Any(e => e.CustomerId == id);
        }
        public async Task<IActionResult> Create2([Bind("PetsId,CustomerId")] wish_list wish_list)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wish_list);
                await _context.SaveChangesAsync();
               
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID", wish_list.CustomerId);
            ViewData["PetsId"] = new SelectList(_context.Pets, "ID", "ID", wish_list.PetsId);
            ViewBag.Id = HttpContext.Session.GetInt32("userID");
            return RedirectToAction("Index","Pets");
        }

        public async Task<IActionResult> Delete2(int PetsId, int CustomerId)
        {

           // var wish_list = await _context.wish_list.FindAsync(PetsId, CustomerId);
            var wish_list = await _context.wish_list
                .Include(w => w.Customer)
                .Include(w => w.pet)
                .FirstOrDefaultAsync(m => m.CustomerId == CustomerId  && m.PetsId == PetsId);
            _context.wish_list.Remove(wish_list);
            await _context.SaveChangesAsync();
            ViewBag.Id = HttpContext.Session.GetInt32("userID");
            return RedirectToAction("Index", "Pets");
        }
      
    }
}
