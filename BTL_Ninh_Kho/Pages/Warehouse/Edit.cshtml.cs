using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(ApplicationDbContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public EditWarehouseRequest Input { get; set; }

        public SelectList Managers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                .Include(w => w.Manager)
                .FirstOrDefaultAsync(w => w.ID == id);

            if (warehouse == null)
            {
                return NotFound();
            }

            Input = new EditWarehouseRequest
            {
                ID = warehouse.ID,
                Name = warehouse.Name,
                Address = warehouse.Address,
                Capacity = warehouse.Capacity,
                Area = warehouse.Area,
                ManagerId = warehouse.ManagerId,
                IsActive = warehouse.IsActive
            };

            // Load danh sách nhân viên cho dropdown
            var managers = await _context.Employees
                .Include(e => e.Role)
                .Select(e => new { e.ID, Name = $"{e.Name} ({e.Role.Name})" })
                .ToListAsync();

            Managers = new SelectList(managers, "ID", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation($"Bắt đầu cập nhật kho có ID: {Input.ID}");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model không hợp lệ");
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning($"Model Error: {modelError.ErrorMessage}");
                }

                // Load lại danh sách manager nếu model không hợp lệ
                var managers = await _context.Employees
                    .Include(e => e.Role)
                    .Select(e => new { e.ID, Name = $"{e.Name} ({e.Role.Name})" })
                    .ToListAsync();

                Managers = new SelectList(managers, "ID", "Name");
                return Page();
            }

            try
            {
                _logger.LogInformation("Tìm kiếm kho trong database");
                var warehouse = await _context.Warehouses
                    .Include(w => w.Manager)
                    .FirstOrDefaultAsync(w => w.ID == Input.ID);

                if (warehouse == null)
                {
                    _logger.LogWarning($"Không tìm thấy kho có ID: {Input.ID}");
                    return NotFound();
                }

                _logger.LogInformation("Cập nhật thông tin kho");
                _logger.LogInformation($"Tên cũ: {warehouse.Name} -> Tên mới: {Input.Name}");
                _logger.LogInformation($"Địa chỉ cũ: {warehouse.Address} -> Địa chỉ mới: {Input.Address}");
                _logger.LogInformation($"Sức chứa cũ: {warehouse.Capacity} -> Sức chứa mới: {Input.Capacity}");
                _logger.LogInformation($"Diện tích cũ: {warehouse.Area} -> Diện tích mới: {Input.Area}");
                _logger.LogInformation($"Quản lý cũ: {warehouse.ManagerId} -> Quản lý mới: {Input.ManagerId}");
                _logger.LogInformation($"Trạng thái cũ: {warehouse.IsActive} -> Trạng thái mới: {Input.IsActive}");

                // Cập nhật thông tin kho
                warehouse.Name = Input.Name;
                warehouse.Address = Input.Address;
                warehouse.Capacity = Input.Capacity;
                warehouse.Area = Input.Area;
                warehouse.ManagerId = Input.ManagerId;
                warehouse.IsActive = Input.IsActive;

                _logger.LogInformation("Đánh dấu entity đã thay đổi");
                _context.Update(warehouse);

                _logger.LogInformation("Lưu thay đổi vào database");
                await _context.SaveChangesAsync();

                _logger.LogInformation("Cập nhật thành công");
                TempData["SuccessMessage"] = "Cập nhật thông tin kho thành công!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật thông tin kho");
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật thông tin kho. Vui lòng thử lại.");

                // Load lại danh sách manager
                var managers = await _context.Employees
                    .Include(e => e.Role)
                    .Select(e => new { e.ID, Name = $"{e.Name} ({e.Role.Name})" })
                    .ToListAsync();

                Managers = new SelectList(managers, "ID", "Name");
                return Page();
            }
        }
    }

    public class EditWarehouseRequest
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên kho")]
        [StringLength(225, ErrorMessage = "Tên kho không được vượt quá 225 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập sức chứa")]
        [Range(1, int.MaxValue, ErrorMessage = "Sức chứa phải lớn hơn 0")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập diện tích")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Diện tích phải lớn hơn 0")]
        public double Area { get; set; }

        public bool IsActive { get; set; }

        public int? ManagerId { get; set; }
    }
} 