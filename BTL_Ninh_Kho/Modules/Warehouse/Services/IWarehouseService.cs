using BTL_Ninh_Kho.Modules.Warehouse.Models;

namespace BTL_Ninh_Kho.Modules.Warehouse.Services
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Models.Warehouse>> GetAllWarehousesAsync();
        Task<Models.Warehouse> GetWarehouseByIdAsync(int id);
        Task<Models.Warehouse> CreateWarehouseAsync(Models.Warehouse warehouse);
        Task<Models.Warehouse> UpdateWarehouseAsync(Models.Warehouse warehouse);
        Task<bool> DeleteWarehouseAsync(int id);
        Task<bool> UpdateWarehouseStatusAsync(int id, bool isActive);
    }
} 