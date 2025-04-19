using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ApplicationDbContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public CreateWarehouseRequest Input { get; set; }

        public SelectList Managers { get; set; }

        public void OnGet()
        {
            try
            {
                _logger.LogInformation("Bắt đầu lấy danh sách nhân viên");

                // Kiểm tra kết nối database
                var canConnect = _context.Database.CanConnect();
                _logger.LogInformation($"Kết nối database: {(canConnect ? "Thành công" : "Thất bại")}");

                // Lấy danh sách tất cả nhân viên đang hoạt động
                var employees = _context.Employees
                    .Include(e => e.Role)
                    .ToList();

                _logger.LogInformation($"Tìm thấy {employees.Count} nhân viên trong database");
                
                foreach (var emp in employees)
                {
                    _logger.LogInformation($"Nhân viên: ID={emp.ID}, Tên={emp.Name}, Role={emp.Role?.Name}, Status={emp.Status}");
                }

                var managers = employees
                    .Select(e => new { e.ID, Name = $"{e.Name} ({e.Role.Name})" })
                    .ToList();

                _logger.LogInformation($"Số lượng nhân viên sau khi chọn: {managers.Count}");

                if (managers.Any())
                {
                    Managers = new SelectList(managers, "ID", "Name");
                    _logger.LogInformation("Đã tạo SelectList thành công");
                }
                else
                {
                    _logger.LogWarning("Không tìm thấy nhân viên nào");
                    Managers = new SelectList(Enumerable.Empty<SelectListItem>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách nhân viên");
                Managers = new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Nếu model không hợp lệ, load lại danh sách manager
                var managers = _context.Employees
                    .Include(e => e.Role)
                    .Select(e => new { e.ID, Name = $"{e.Name} ({e.Role.Name})" })
                    .ToList();
                Managers = new SelectList(managers, "ID", "Name");
                return Page();
            }

            var warehouse = new BTL_Ninh_Kho.Modules.Warehouse.Models.Warehouse
            {
                Name = Input.Name,
                Address = Input.Address,
                Capacity = Input.Capacity,
                Area = Input.Area,
                IsActive = Input.IsActive,
                ManagerId = Input.ManagerId
            };

            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
} 