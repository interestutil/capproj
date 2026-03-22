using Microsoft.AspNetCore.Mvc;
using capproj.Repositories;
using capproj.Models;

namespace capproj.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrderRepository _orderRepo;

        public DashboardController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<IActionResult> Index()
        {
            var all = await _orderRepo.GetAllAsync();
            var now = DateTime.UtcNow;

            var vm = new DashboardViewModel
            {
                UnprocessedCount = all.Count(o => !o.Status),
                OrdersThisMonth = all.Count(o => o.CreatedAt.Year == now.Year && o.CreatedAt.Month == now.Month),
                LastFiveOrders = all.OrderByDescending(o => o.CreatedAt).Take(5).ToList()
            };

            return View(vm);
        }
    }
}
