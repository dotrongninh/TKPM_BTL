using Microsoft.EntityFrameworkCore;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;

namespace BTL_Ninh_Kho.Modules.Warehouse.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly ApplicationDbContext _context;

        public WarehouseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Warehouse>> GetAllWarehousesAsync()
        {
            return await _context.Warehouses.ToListAsync();
        }

        public async Task<Models.Warehouse> GetWarehouseByIdAsync(int id)
        {
            return await _context.Warehouses.FindAsync(id);
        }

        public async Task<Models.Warehouse> CreateWarehouseAsync(Models.Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<Models.Warehouse> UpdateWarehouseAsync(Models.Warehouse warehouse)
        {
            _context.Entry(warehouse).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> DeleteWarehouseAsync(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
                return false;

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateWarehouseStatusAsync(int id, bool isActive)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
                return false;

            warehouse.IsActive = isActive;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 