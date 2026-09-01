using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            // sample data for dashboard
            var vm = new DashboardViewModel
            {
                Outstanding = 399630.00m,
                ProfitEarned = 6300.00m,
                AwaitingBill = 399630.00m,
                StockOnHand = 2813,
                Alerts = new List<StockAlertItem>
                {
                    new StockAlertItem { ProductName = "18\" PEDESTAL ALU", Units = 0 },
                    new StockAlertItem { ProductName = "CU FITTING", Units = 0 },
                    new StockAlertItem { ProductName = "FARRATA PEDESTAL", Units = 0 },
                    new StockAlertItem { ProductName = "HELIX PEDESTAL", Units = 0 },
                    new StockAlertItem { ProductName = "MAX AIR PEDESTAL", Units = 0 }
                }
            };

            return View(vm);
        }
    }
}
