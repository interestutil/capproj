using System.Collections.Generic;

namespace capproj.Models
{
    public class DashboardViewModel
    {
        public int UnprocessedCount { get; set; }
        public int OrdersThisMonth { get; set; }
        public List<Order> LastFiveOrders { get; set; } = new();
    }
}
