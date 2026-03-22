using Microsoft.AspNetCore.Mvc;
using capproj.Models;
using capproj.Repositories;

namespace capproj.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IShelfRepository _shelfRepo;

        public OrdersController(IOrderRepository orderRepo, IShelfRepository shelfRepo)
        {
            _orderRepo = orderRepo;
            _shelfRepo = shelfRepo;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var list = await _orderRepo.GetAllAsync();
            return View(list);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            var shelves = await _shelfRepo.GetAllAsync();
            ViewBag.Shelves = shelves;
            return View(new Order());
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (order.orderLines == null || !order.orderLines.Any())
            {
                ModelState.AddModelError(string.Empty, "Please add at least one order line.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Shelves = await _shelfRepo.GetAllAsync();
                return View(order);
            }

            // Ensure CreatedAt is set
            order.CreatedAt = DateTime.UtcNow;

            await _orderRepo.AddAsync(order);
            return RedirectToAction("Index", "Orders");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();
            ViewBag.Shelves = await _shelfRepo.GetAllAsync();
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.Shelves = await _shelfRepo.GetAllAsync();
                return View(order);
            }

            await _orderRepo.UpdateAsync(order);
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return NotFound();
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
