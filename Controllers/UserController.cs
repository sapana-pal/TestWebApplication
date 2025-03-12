using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class UserController : Controller
    {
        private DemoEntities _context = new DemoEntities();
        [HttpGet]
        public IActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database (assume _context is your DbContext)
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.Find(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Update fields manually
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.Address = user.Address;
                existingUser.IsActive = user.IsActive;
                existingUser.UpdatedAt = DateTime.UtcNow; // Set update timestamp

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

    }
}