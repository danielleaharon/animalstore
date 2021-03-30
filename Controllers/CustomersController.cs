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
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Net;


namespace Animal_Store.Controllers
{
 
    public class CustomersController : Controller
    {
      
        private readonly Animal_StoreContext _context;

        public CustomersController(Animal_StoreContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
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
                return View(await _context.Customer.ToListAsync());
            }
           
            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";
              
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }


           
            return RedirectToAction("Index","Home");
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
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
                return View(customer);
            }

            else if (HttpContext.Session.GetString("Type") == "Owner")
            {
                ViewBag.MyPets = "My Pets";

            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "wish list";

            }
            if (HttpContext.Session.GetInt32("userID") != id)
            {

                return RedirectToAction("Index", "Home");
            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            return View(customer);

           

           
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Type") == null|| HttpContext.Session.GetString("Type") == "user" )
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

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Username,Password,Usertype")] Customer customer)
        {
            if (HttpContext.Session.GetString("Type") == null)
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
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "'wish list";

            }
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            return RedirectToAction("Login");
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            ViewBag.Type = HttpContext.Session.GetString("Type");
            ViewBag.Name = HttpContext.Session.GetString("userName");
            if (HttpContext.Session.GetString("Type") == null|| HttpContext.Session.GetString("user") == null)
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
                return View(customer);
            }
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "'wish list";


            }
            if (HttpContext.Session.GetInt32("userID") != id)
            {

                return RedirectToAction("Index", "Home");
            }
         
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Username,Password,Usertype")] Customer customer)
        {
            if (HttpContext.Session.GetString("Type") == null)
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
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "'wish list";

            }
            if (id != customer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.ID))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.ID == id);
        }
        public IActionResult Login1()
        {
            return View();
        }
        public IActionResult Login(string username, string pass)
        {
            var user = _context.Customer.FirstOrDefault(u => u.Username == username && u.Password == pass);

            if (user != null)
            {
                SignIn(user);

                ViewBag.message = null;
                ViewBag.message2 = "Hi " + HttpContext.Session.GetString("userName").ToString() + " Log out";
                return RedirectToAction("Index", "Home");

            }
            else
            {
                var user1 = _context.Customer.FirstOrDefault(u => u.Username == username);
                if (user1 != null)
                {
                    ViewBag.ErrorUser = "worng pass";
                }
                else
                {
                    ViewBag.ErrorUser = "User does not exist";
                }



            }

            return View("Login1");

        }

        private void SignIn(Customer user)
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("Type", user.Usertype);
            HttpContext.Session.SetString("userName", user.Name);
            HttpContext.Session.SetInt32("userID", user.ID);
            if (user.Usertype.Equals("Owner"))
            {
                var a = from store in _context.Stores
                        where store.StoreName.Equals(user.Name)
                        select store;
                if (a != null)
                {
                    HttpContext.Session.SetInt32("StoreID", a.First().ID);
                   
                }
            }

        }

        public IActionResult Register()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("ID,Name,Username,Password,Usertype")] Customer customer)
        {


            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                SignIn(customer);
                return RedirectToAction("Login1");
            }
            return RedirectToAction("Register");

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Type");
            HttpContext.Session.Remove("userName");
            HttpContext.Session.Remove("userID");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }
        public IActionResult SearchCustomer(string CustumerName, string CustumerUser, string Typeuser)
        {
            ViewBag.userId = HttpContext.Session.GetInt32("userID");
            if (HttpContext.Session.GetString("Type") == null)
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
            else if (HttpContext.Session.GetString("Type") == "userRegistered")
            {

                ViewBag.wish = "'wish list";

            }
            if (CustumerName == null && CustumerUser == null && Typeuser == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          select cus;
                return View("Index", res.ToList());
            }
            else if (CustumerName == null && CustumerUser == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Usertype.Contains(Typeuser)
                          select cus;
                return View("Index", res.ToList());
            }
            else if (CustumerName == null &&  Typeuser == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Username.Contains( CustumerUser)
                          select cus;
                return View("Index", res.ToList());
            }
            else if ( CustumerUser == null && Typeuser == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Name.Contains(CustumerName)
                          select cus;
                return View("Index", res.ToList());
            }
            else if ( Typeuser == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Name.Contains(CustumerName) &&   cus.Username.Contains(CustumerUser)
                          select cus;
                return View("Index", res.ToList());
            }
            else if (CustumerName == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Usertype.Contains(Typeuser) && cus.Username.Contains(CustumerUser)
                          select cus;
                return View("Index", res.ToList());
            }
            else if (CustumerUser == null)
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Usertype.Contains(Typeuser) && cus.Name.Contains(CustumerName)
                          select cus;
                return View("Index", res.ToList());
            }
            else
            {
                var res = from cus in _context.Customer.Include(w => w.wish_list)
                          where cus.Usertype.Contains(Typeuser) && cus.Name.Contains(CustumerName) && cus.Username.Contains(CustumerUser)
                          select cus;
                return View("Index", res.ToList());
            }



        }


    }
}
