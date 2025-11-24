namespace APIKarakatsiya.Models.DTOs.SaleDto
{
    public class SaleFilterDto
    {
        public string? UserId { get; set; }
        public decimal? MinProfit { get; set; }
        public decimal? MaxProfit { get; set; }

        // Фильтр по периоду
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Сортировка
        public string? SortBy { get; set; }
    }
}
