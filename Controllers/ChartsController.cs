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
using System.Collections.ObjectModel;

namespace Animal_Store.Controllers
{
    public class ChartsController : Controller
    {
        private readonly Animal_StoreContext _context;

        public ChartsController(Animal_StoreContext context)
        {
            _context = context;
        }

        // GET: Charts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Charts.ToListAsync());
        }

        // GET: Charts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charts = await _context.Charts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (charts == null)
            {
                return NotFound();
            }

            return View(charts);
        }

        // GET: Charts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Charts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,Counter")] Charts charts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(charts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(charts);
        }

        // GET: Charts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charts = await _context.Charts.FindAsync(id);
            if (charts == null)
            {
                return NotFound();
            }
            return View(charts);
        }

        // POST: Charts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type,Counter")] Charts charts)
        {
            if (id != charts.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(charts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChartsExists(charts.ID))
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
            return View(charts);
        }

        // GET: Charts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charts = await _context.Charts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (charts == null)
            {
                return NotFound();
            }

            return View(charts);
        }

        // POST: Charts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var charts = await _context.Charts.FindAsync(id);
            _context.Charts.Remove(charts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChartsExists(int id)
        {
            return _context.Charts.Any(e => e.ID == id);
        }
        public ActionResult BasicStatistics()
        {

            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {

                ViewBag.message = "Log in";
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Name = HttpContext.Session.GetString("userName");

            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "'wish list";

            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            ICollection<Stat> mylist = new Collection<Stat>();

            foreach (var v in _context.Charts)
            {
                mylist.Add(new Stat(v.Type, v.Counter));

            }

            ViewBag.data = mylist;
            ICollection<Stat> mylist2 = new Collection<Stat>();

            var c = from store in _context.Stores
                    select store.Pets.Count;
            var a = c.ToArray();
            int i = 0;
            foreach (var s in _context.Stores)
            {
                mylist2.Add(new Stat(s.StoreName, a[i]));
                i++;
            }



            ViewBag.data2 = mylist2;



            return View();
        }

    }




    public class Stat
    {
        public string Key;
        public int Values;


        public Stat(string key, int values)
        {
            Key = key;
            Values = values;
        }
    }



}
