namespace UI.Models
{
    public class DashboardViewModel
    {
        public decimal Outstanding { get; set; }
        public decimal ProfitEarned { get; set; }
        public decimal AwaitingBill { get; set; }
        public int StockOnHand { get; set; }
        public List<StockAlertItem> Alerts { get; set; } = new();
    }

    public class StockAlertItem
    {
        public string ProductName { get; set; } = string.Empty;
        public int Units { get; set; }
    }
}
