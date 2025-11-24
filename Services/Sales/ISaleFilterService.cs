using APIKarakatsiya.Models.DTOs.SaleDto;

namespace APIKarakatsiya.Services.Sales
{
    public interface ISaleFilterService
    {
        Task<SaleFilterResultDto> FilterAsync(SaleFilterDto filter);
    }
}