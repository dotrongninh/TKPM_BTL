using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class CreateImportModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateImportModel> _logger;

        public CreateImportModel(ApplicationDbContext context, ILogger<CreateImportModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public ImportRequest Input { get; set; }

        public SelectList Suppliers { get; set; }
        public SelectList Warehouses { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            // Load danh sách nhà cung cấp
            var suppliers = await _context.Suppliers.ToListAsync();
            Suppliers = new SelectList(suppliers, "ID", "Name");

            // Load danh sách kho
            var warehouses = await _context.Warehouses
                .Where(w => w.IsActive)
                .ToListAsync();
            Warehouses = new SelectList(warehouses, "ID", "Name");

            // Load danh sách hàng hóa và đơn vị tính
            Products = await _context.Products
                .Include(p => p.Unit)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            try
            {
                // Tạo đơn nhập mới
                var importRequest = new BTL_Ninh_Kho.Modules.Warehouse.Models.ImportRequest
                {
                    SupplierId = Input.SupplierId,
                    WarehouseId = Input.WarehouseId,
                    ImportDate = DateTime.Now,
                    Status = 1 // Chờ duyệt
                };

                _context.ImportRequests.Add(importRequest);
                await _context.SaveChangesAsync();

                // Thêm chi tiết đơn nhập
                foreach (var detail in Input.Details)
                {
                    var importDetail = new ImportRequestDetail
                    {
                        ImportRequestId = importRequest.ID,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity,
                        ImportPrice = detail.ImportPrice
                    };
                    _context.ImportRequestDetails.Add(importDetail);
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Tạo đơn nhập kho thành công!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo đơn nhập kho");
                ModelState.AddModelError("", "Có lỗi xảy ra khi tạo đơn nhập kho. Vui lòng thử lại.");
                await OnGetAsync();
                return Page();
            }
        }
    }
} 