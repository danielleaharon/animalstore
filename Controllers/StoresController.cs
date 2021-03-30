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
    public class StoresController : Controller
    {
        private readonly Animal_StoreContext _context;

        public StoresController(Animal_StoreContext context)
        {
            _context = context;
        }

        // GET: Stores
        public async Task<IActionResult> Index()
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
                return View(await _context.Stores.ToListAsync());
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                    ViewBag.MyPets = "My Pets";
            }
             else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");

            return View(await _context.Stores.ToListAsync());
        }

        // GET: Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
                return View(await _context.Stores.ToListAsync());
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stores == null)
            {
                return NotFound();
            }

            return View(stores);
        }

        // GET: Stores/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
                return View();
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Location,StoreName")] Stores stores)
        {
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
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

                ViewBag.wish = "wish list";

            }
            if (ModelState.IsValid)
            {
                _context.Add(stores);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            return View(stores);
        }

        // GET: Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores.FindAsync(id);
            if (stores == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
                return View(stores);
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
                return View(stores);
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            return RedirectToAction("Index", "Home");
          
        }

        // POST: Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Location,StoreName")] Stores stores)
        {
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
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

                ViewBag.wish = "wish list";

            }
            if (id != stores.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoresExists(stores.ID))
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
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            return View(stores);
        }

        // GET: Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            if (id == null)
            {
                return NotFound();
            }

            var stores = await _context.Stores
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stores == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

                //return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
                return View(stores);
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
                return View(stores);
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var stores = await _context.Stores.FindAsync(id);
            _context.Stores.Remove(stores);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoresExists(int id)
        {
            return _context.Stores.Any(e => e.ID == id);
        }
        public IActionResult Search(string name, string Location, int? count)
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                ViewBag.message = "Log in";

            }
            else
            {
                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";

            }
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

                ViewBag.wish = "wish list";

            }
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Name = HttpContext.Session.GetString("userName");
            if (name == null && Location == null && count == null)
            {
                var r = from store in _context.Stores
                        select store;
                return View("Index", r.ToList());
            }
            else if (name == null && Location == null)
            {
                var r = from store in _context.Stores
                        where store.Pets.Count >= count
                        select store;
                return View("Index", r.ToList());
            }
            else if (name == null && count == null)
            {
                var r = from store in _context.Stores
                        where store.Location.Contains(Location)
                        select store;
                return View("Index", r.ToList());
            }
            else if (Location == null && count == null)
            {
                var r = from store in _context.Stores
                        where store.StoreName.Contains(name)
                        select store;
                return View("Index", r.ToList());
            }
            else if (name == null)
            {
                var r = from store in _context.Stores
                        where store.Location.Contains(Location) && store.Pets.Count >= count
                        select store;
                return View("Index", r.ToList());
            }
            else if (Location == null)
            {
                var r = from store in _context.Stores
                        where store.StoreName.Contains(name) && store.Pets.Count >= count
                        select store;
                return View("Index", r.ToList());
            }
            else if (count == null)
            {
                var r = from store in _context.Stores
                        where store.StoreName.Contains(name) && store.Location.Contains(Location)
                        select store;
                return View("Index", r.ToList());
            }
            else
            {
                var r = from store in _context.Stores
                        where store.Location.Contains(Location) && store.Pets.Count >= count && store.StoreName.Contains(name)
                        select store;
                return View("Index", r.ToList());
            }


        }

    }
}
