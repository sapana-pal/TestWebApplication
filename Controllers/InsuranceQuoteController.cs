using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class InsuranceQuoteController : Controller
    {
        private readonly DemoEntities _db;

        public InsuranceQuoteController(DemoEntities db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var quotes = await _db.InsuranceQuotes.ToListAsync();
            return View(quotes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceQuoteRequest quote)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Failed to create quote.";
                return View(quote);
            }

            bool exists = await _db.InsuranceQuotes.AnyAsync(q => q.VehicleRegistrationNumber == quote.VehicleRegistrationNumber);
            if (exists)
            {
                ViewBag.Message = "Insurance already exists for this vehicle.";
                return View(quote);
            }

            quote.CreatedAt = DateTime.UtcNow;
            _db.InsuranceQuotes.Add(quote);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var quote = await _db.InsuranceQuotes.FindAsync(id);
            if (quote == null) return NotFound();
            return View(quote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InsuranceQuoteRequest updatedQuote)
        {
            if (!ModelState.IsValid) return View(updatedQuote);

            var existingQuote = await _db.InsuranceQuotes.FindAsync(updatedQuote.Id);
            if (existingQuote == null) return NotFound();

            existingQuote.VehicleType = updatedQuote.VehicleType;
            existingQuote.FullName = updatedQuote.FullName;
            existingQuote.MobileNumber = updatedQuote.MobileNumber;
            existingQuote.VehicleRegistrationNumber = updatedQuote.VehicleRegistrationNumber;

            _db.Entry(existingQuote).State = (System.Data.Entity.EntityState)Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var quote = await _db.InsuranceQuotes.FindAsync(id);
            if (quote == null) return NotFound();

            _db.InsuranceQuotes.Remove(quote);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
