namespace APIKarakatsiya.Models.DTOs.SaleDto
{
    public class SaleFilterResultDto
    {
        public List<SaleDto> Sales { get; set; } = new List<SaleDto>();
        public decimal TotalProfit { get; set; }
    }
}
