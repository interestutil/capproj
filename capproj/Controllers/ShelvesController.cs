using Microsoft.AspNetCore.Mvc;
using capproj.Models;
using capproj.Repositories;

namespace capproj.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly IShelfRepository _shelfRepo;

        public ShelvesController(IShelfRepository shelfRepo)
        {
            _shelfRepo = shelfRepo;
        }

        // GET: Shelves
        public async Task<IActionResult> Index()
        {
            var list = await _shelfRepo.GetAllAsync();
            return View(list);
        }

        // GET: Shelves/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var shelf = await _shelfRepo.GetByIdAsync(id);
            if (shelf == null) return NotFound();
            return View(shelf);
        }

        // GET: Shelves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shelves/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shelf shelf)
        {
            if (!ModelState.IsValid)
                return View(shelf);

            await _shelfRepo.AddAsync(shelf);
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Shelves/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var shelf = await _shelfRepo.GetByIdAsync(id);
            if (shelf == null) return NotFound();
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shelf shelf)
        {
            if (id != shelf.Id) return BadRequest();
            if (!ModelState.IsValid) return View(shelf);
            await _shelfRepo.UpdateAsync(shelf);
            return RedirectToAction(nameof(Index));
        }

        // GET: Shelves/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var shelf = await _shelfRepo.GetByIdAsync(id);
            if (shelf == null) return NotFound();
            return View(shelf);
        }

        // POST: Shelves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _shelfRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
