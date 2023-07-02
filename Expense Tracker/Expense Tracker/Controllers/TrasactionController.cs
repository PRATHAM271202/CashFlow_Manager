using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Models;

namespace Expense_Tracker.Controllers
{
    public class TrasactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrasactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trasaction
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trasactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

    

        // GET: Trasaction/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            PopulateCategories();
            if(id==0)
            return View(new Trasaction());
            else
                return View(_context.Trasactions.Find(id));
        }

        // POST: Trasaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TrasactionId,CategoryId,Amount,Note,Date")] Trasaction trasaction)
        {
            if (ModelState.IsValid)
            {   
                if(trasaction.TrasactionId==0)
                _context.Add(trasaction);
                else 
                    _context.Update(trasaction);    
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(trasaction);
        }

      

       

        // POST: Trasaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trasactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trasactions'  is null.");
            }
            var trasaction = await _context.Trasactions.FindAsync(id);
            if (trasaction != null)
            {
                _context.Trasactions.Remove(trasaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}
