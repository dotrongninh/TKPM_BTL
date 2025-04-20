using Microsoft.AspNetCore.Mvc.RazorPages;
using BTL_Ninh_Kho.Modules.Warehouse.Services;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class IndexModel : PageModel
    {
        private readonly IWarehouseService _warehouseService;

        public IndexModel(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        public IEnumerable<BTL_Ninh_Kho.Modules.Warehouse.Models.Warehouse>? Warehouses { get; set; }

        public async Task OnGetAsync()
        {
            Warehouses = await _warehouseService.GetAllWarehousesAsync();
        }
    }
} 