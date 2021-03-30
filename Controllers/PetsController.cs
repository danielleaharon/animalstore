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
    public class PetsController : Controller
    {
        private readonly Animal_StoreContext _context;

        public PetsController(Animal_StoreContext context)
        {
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index()
        {
            var animal_StoreContext = _context.Pets.Include(p => p.Store).Include(t=>t.wish_list);
            if (HttpContext.Session.GetString("Type") == null || HttpContext.Session.GetString("Type") == "user")
            {
                HttpContext.Session.SetString("Type", "user");
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

                ViewBag.wish = "wish list";

            }
            int sum = 0;
            string chosenType = null;
            foreach (Pets p in _context.Pets.Include(p => p.Store))
            {
                if (HttpContext.Session.GetInt32(p.Type) != null)
                {
                    if (HttpContext.Session.GetInt32(p.Type) > sum)
                    {
                        chosenType = p.Type;
                        sum = HttpContext.Session.GetInt32(p.Type).Value;
                    }
                }
            }
            ViewBag.chosenType = chosenType;

            ViewBag.Id = HttpContext.Session.GetInt32("userID");
            ViewBag.userId = HttpContext.Session.GetInt32("userID");

            return View(await animal_StoreContext.ToListAsync());
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pets = await _context.Pets
                 .Include(p => p.Store)
                 .FirstOrDefaultAsync(m => m.ID == id);
            if (pets == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("Type") == "user"|| HttpContext.Session.GetString("Type") == "userRegistered")
            {
                string type = pets.Type;
                if (HttpContext.Session.GetInt32(pets.Type) != null)
                {
                    int count = HttpContext.Session.GetInt32(pets.Type).Value;
                    count++;
                    HttpContext.Session.SetInt32(pets.Type, count);
                }
                else
                {
                    HttpContext.Session.SetInt32(pets.Type, 1);
                }
                var charts = _context.Charts.FirstOrDefault(u => u.Type == pets.Type);
                if (charts != null)
                {
                    charts.Counter++;
                    await _context.SaveChangesAsync();

                }

            }
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Name = HttpContext.Session.GetString("userName");
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
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


            return View(pets);
        }

        // GET: Pets/Create
        public IActionResult Create()
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
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.wish = _context.wish_list.ToList();
            ViewBag.Id = HttpContext.Session.GetString("userID");
            ViewData["StoreId"] = new SelectList(_context.Set<Stores>(), "ID", "ID");
            return RedirectToAction("Index", "Home");

        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Type,Gender,Age,LifeExpectancy,StoreId,img")] Pets pets)
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
                _context.Add(pets);
                await _context.SaveChangesAsync();
                var user = _context.Charts.FirstOrDefault(u => u.Type == pets.Type);
               
                if (user== null)
                {
                    Charts newChart = new Charts();
                    newChart.Type = pets.Type;
                    newChart.Counter = 0;
                    _context.Charts.Add(newChart);
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction(nameof(Index));
            }
            if (HttpContext.Session.GetInt32(pets.Type) == null)
            {
                HttpContext.Session.SetInt32(pets.Type, 0);
            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Id = HttpContext.Session.GetString("userID");
            ViewData["StoreId"] = new SelectList(_context.Set<Stores>(), "ID", "ID", pets.StoreId);
            return View(pets);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pets = await _context.Pets.FindAsync(id);
            if (pets == null)
            {
                return NotFound();
            }
            //  ViewData["CustomerId"] = new SelectList(_context.Customer, "ID", "ID", pets.CustomerId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "ID", "ID", pets.StoreId);
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Id = HttpContext.Session.GetString("userID");
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            ViewBag.StoreId = HttpContext.Session.GetInt32("StoreID");
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
                return View(pets);
            }
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
                return View(pets);
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
           
            return RedirectToAction("Index", "Home");
       
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Type,Gender,Age,LifeExpectancy,StoreId,img")] Pets pets)
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
            if (id != pets.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetsExists(pets.ID))
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
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Id = HttpContext.Session.GetString("userID");
            ViewData["StoreId"] = new SelectList(_context.Set<Stores>(), "ID", "ID", pets.StoreId);
            return View(pets);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pets = await _context.Pets
              .Include(p => p.Store)
              .FirstOrDefaultAsync(m => m.ID == id);
            if (pets == null)
            {
                return NotFound();
            }

            if (HttpContext.Session.GetString("Type") == "admin")
            {
                ViewBag.msg = "Customer";
                return View(pets);
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
            return RedirectToAction("Index", "Home");
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pets = await _context.Pets.FindAsync(id);
            _context.Pets.Remove(pets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetsExists(int id)
        {
            return _context.Pets.Any(e => e.ID == id);
        }

        public IActionResult SearchPets(string storeName, string PetsType, string Gender, int? age)
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

                ViewBag.wish = "wish list";

            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            int sum = 0;
            string chosenType = null;
            foreach (Pets p in _context.Pets.Include(p => p.Store))
            {
                if (HttpContext.Session.GetInt32(p.Type) != null)
                {
                    if (HttpContext.Session.GetInt32(p.Type) > sum)
                    {
                        chosenType = p.Type;
                        sum = HttpContext.Session.GetInt32(p.Type).Value;
                    }
                }
            }
            ViewBag.chosenType = chosenType;

            ViewBag.Id = HttpContext.Session.GetInt32("userID");
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Name = HttpContext.Session.GetString("userName");
            if (storeName == null && PetsType == null && Gender == null && age == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        select pets;

                return View("Index", r.ToList());
            }
            if (PetsType == null && Gender == null && age == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName)
                        select pets;
                return View("Index", r.ToList());
            }
            if (storeName == null && Gender == null && age == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Type.Contains(PetsType)
                        select pets;

                return View("Index", r.ToList());
            }
            if (storeName == null && PetsType == null && age == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Gender.Contains(Gender)
                        select pets;
                return View("Index", r.ToList());
            }
            if (storeName == null && PetsType == null && Gender == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Age >= age
                        select pets;
                return View("Index", r.ToList());
            }
            if (PetsType == null && Gender == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            if (age == null && Gender == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Type.Contains(PetsType)
                        select pets;

                return View("Index", r.ToList());
            }
            if (age == null && PetsType == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Gender.Contains(Gender)
                        select pets;

                return View("Index", r.ToList());
            }
            if (age == null && PetsType == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Gender.Contains(Gender)
                        select pets;

                return View("Index", r.ToList());
            }
            if (age == null && storeName == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Type.Contains(PetsType) && pets.Gender.Contains(Gender)
                        select pets;

                return View("Index", r.ToList());
            }
            if (Gender == null && storeName == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Type.Contains(PetsType) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            if (PetsType == null && storeName == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Gender.Contains(Gender) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            if (PetsType == null && storeName == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Gender.Contains(Gender) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            if (age == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Type.Contains(PetsType) && pets.Gender.Contains(Gender)
                        select pets;

                return View("Index", r.ToList());
            }
            if (Gender == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Type.Contains(PetsType) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            if (PetsType == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Store.StoreName.Contains(storeName) && pets.Gender.Contains(Gender) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            if (storeName == null)
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Type.Contains(PetsType) && pets.Gender.Contains(Gender) && pets.Age >= age
                        select pets;

                return View("Index", r.ToList());
            }
            else
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list)
                        where pets.Type.Contains(PetsType) && pets.Gender.Contains(Gender) && pets.Age >= age && pets.Store.StoreName.Contains(storeName)
                        select pets;

                return View("Index", r.ToList());
            }

        }

        public IActionResult GroupbyGender(string groupby)
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

                ViewBag.wish = "wish list";

            }
            int sum = 0;
            string chosenType = null;
            foreach (Pets p in _context.Pets.Include(p => p.Store))
            {
                if (HttpContext.Session.GetInt32(p.Type) != null)
                {
                    if (HttpContext.Session.GetInt32(p.Type) > sum)
                    {
                        chosenType = p.Type;
                        sum = HttpContext.Session.GetInt32(p.Type).Value;
                    }
                }
            }
            ViewBag.chosenType = chosenType;

            ViewBag.Id = HttpContext.Session.GetInt32("userID");
            if (groupby.Equals("Type"))
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w=> w.wish_list).ToList()
                        group pets by pets.Type into newgroup
                        from pet2 in newgroup
                        select pet2;

                return View("Index", r.ToList());
            }
            else if (groupby.Equals("Gender"))
            {
                var r = from pets in _context.Pets.Include(p => p.Store).Include(w => w.wish_list).ToList()
                        group pets by pets.Gender into newgroup
                        from pet2 in newgroup
                        select pet2;

                return View("Index", r.ToList());
            }
            else

            {

                var r = from store1 in _context.Stores.ToList()
                        join pets in _context.Pets.Include(p => p.Store) on store1.ID equals pets.StoreId into prodGroup
                        from pet2 in prodGroup
                        select pet2;
                return View("Index", r.ToList());
            }
        }
        public IActionResult MyPets()
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

                ViewBag.wish = "wish list";

            }
            ViewBag.Id = HttpContext.Session.GetInt32("userID");
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
          
            var store = from store1 in _context.Stores.Include(w=>w.Pets)
                        join pet1 in _context.Pets.Include(w=>w.Store) on store1.ID equals pet1.StoreId into newGroup
                        from pet2 in newGroup
                        where pet2.StoreId == HttpContext.Session.GetInt32("StoreID")
                        select pet2;
            ViewBag.MyPets = "My Pets";
            return View("Index", store);

        }
        public IActionResult myWish()
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            ViewBag.myWish = "My wish list";
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

                ViewBag.wish = "wish list";

            }
            int sum = 0;
            string chosenType = null;
            foreach (Pets p in _context.Pets.Include(p => p.Store))
            {
                if (HttpContext.Session.GetInt32(p.Type) != null)
                {
                    if (HttpContext.Session.GetInt32(p.Type) > sum)
                    {
                        chosenType = p.Type;
                        sum = HttpContext.Session.GetInt32(p.Type).Value;
                    }
                }
            }
            ViewBag.chosenType = chosenType;

            ViewBag.Id = HttpContext.Session.GetInt32("userID");

            var wishlist = from pet in _context.Pets.Include(w => w.Store).Include(w=>w.wish_list)
                           join wish in _context.wish_list.Include(w => w.Customer).Include(w => w.pet) on pet.ID equals wish.PetsId into newG
                           from cos in newG
                           where cos.CustomerId == HttpContext.Session.GetInt32("userID")
                           select pet;
          

            return View("Index", wishlist);
        }
        public IActionResult ShowWish(int ? id)
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            var userName = from c in _context.Customer
                           where c.ID == id
                           select c;
            ViewBag.myWish = "Wish list of " + userName.First().Name;
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

                ViewBag.wish = "wish list";

            }
            int sum = 0;
            string chosenType = null;
            foreach (Pets p in _context.Pets.Include(p => p.Store))
            {
                if (HttpContext.Session.GetInt32(p.Type) != null)
                {
                    if (HttpContext.Session.GetInt32(p.Type) > sum)
                    {
                        chosenType = p.Type;
                        sum = HttpContext.Session.GetInt32(p.Type).Value;
                    }
                }
            }
            ViewBag.chosenType = chosenType;

            ViewBag.Id = HttpContext.Session.GetInt32("userID");

            var wishlist = from pet in _context.Pets.Include(w => w.Store).Include(w => w.wish_list)
                           join wish in _context.wish_list.Include(w => w.Customer).Include(w => w.pet) on pet.ID equals wish.PetsId into newG
                           from cos in newG
                           where cos.CustomerId == id
                           select pet;


            return View("Index", wishlist);
        }

    }
}
