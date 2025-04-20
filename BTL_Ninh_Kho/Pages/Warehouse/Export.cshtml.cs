using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class ExportModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExportModel> _logger;

        public ExportModel(ApplicationDbContext context, ILogger<ExportModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public ExportRequest Input { get; set; }

        public SelectList Warehouses { get; set; }

        public IEnumerable<Product> Products { get; set; }
        public List<BTL_Ninh_Kho.Modules.Warehouse.Models.ImportRequest> ListExportWarehouse { get; set; }

        public List<BTL_Ninh_Kho.Modules.Warehouse.Models.ExportRequest> ListExport { get; set; }
        // public List<BTL_Ninh_Kho.Modules.Warehouse.Models.ExportRequest> ListExportWarehouse { get; set; }
        public List<ImportRequest> ImportRequestsWithStatus2 { get; set; }
        public List<BTL_Ninh_Kho.Modules.Warehouse.Models.ExportRequest> export { get; set; }


        public async Task OnGetAsync()
        {

            // Load danh sách kho
            var warehouses = await _context.Warehouses
                .Where(w => w.IsActive)
                .ToListAsync();
            Warehouses = new SelectList(warehouses, "ID", "Name");

            // Load danh sách hàng hóa và đơn vị tính
            Products = await _context.Products
                .Include(p => p.Unit)
                .ToListAsync();

            ListExport = await _context.ExportRequests
                .Include(ir => ir.Warehouse)
                .ToListAsync();

           // Lấy danh sách đơn nhập kho có trạng thái phù hợp (ví dụ: trạng thái = 2)
            ListExportWarehouse = await _context.ImportRequests
                .Where(ir => ir.Status == 2) // Lọc trạng thái phù hợp
                .Include(ir => ir.Warehouse) // Load thông tin kho
                .ToListAsync();

        }

                public async Task<IActionResult> OnGetImportDetailsAsync(int id)
        {
            var importRequest = await _context.ImportRequests
                .Include(ir => ir.Details)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(ir => ir.ID == id);
        
            if (importRequest == null || importRequest.Details == null || !importRequest.Details.Any())
            {
                return Content("<tr><td colspan='7' class='text-center'>Không có dữ liệu</td></tr>", "text/html");
            }
        
            var html = new System.Text.StringBuilder();
            foreach (var detail in importRequest.Details)
            {
                html.Append("<tr>");
                html.Append($"<td>{detail.ID}</td>");
                html.Append($"<td>{detail.Product?.Name}</td>");
                html.Append($"<td>{detail.Product?.Unit}</td>");
                html.Append($"<td>{detail.Quantity}</td>");
                html.Append($"<td>{detail.Quantity}</td>");
                html.Append($"<td>{detail.Quantity}</td>");
                html.Append($"<td><button class='btn btn-danger btn-sm'>Xóa</button></td>");
                html.Append("</tr>");
            }
        
            return Content(html.ToString(), "text/html");
        }
           
            public async Task<IActionResult> OnGetExportDetailsAsync(int id)
            {
                var exportRequest = await _context.ExportRequests
                    .Include(er => er.Details)
                        .ThenInclude(d => d.Product)
                    .FirstOrDefaultAsync(er => er.ID == id);
            
                if (exportRequest == null || exportRequest.Details == null || !exportRequest.Details.Any())
                {
                    return Content("<tr><td colspan='7' class='text-center'>Không có dữ liệu</td></tr>", "text/html");
                }
            
                var html = new System.Text.StringBuilder();
                foreach (var detail in exportRequest.Details)
                {
                    html.Append("<tr>");
                    html.Append($"<td>{detail.ID}</td>");
                    html.Append($"<td>{detail.Product?.Name}</td>");
                    html.Append($"<td>{detail.Product?.Unit}</td>");
                    html.Append($"<td>{detail.Quantity}</td>");
                    html.Append($"<td>{detail.Quantity}</td>");
                    html.Append($"<td>{detail.Quantity}</td>");
                    html.Append($"<td><button class='btn btn-danger btn-sm'>Xóa</button></td>");
                    html.Append("</tr>");
                }
            
                return Content(html.ToString(), "text/html");
            }
            public async Task<IActionResult> OnGetAvailableWarehousesAsync(int excludeWarehouseId)
            {
                var warehouses = await _context.Warehouses
                    .Where(w => w.ID != excludeWarehouseId && w.IsActive) // Lọc kho ngoại trừ kho ban đầu
                    .ToListAsync();

                return new JsonResult(warehouses.Select(w => new
                {
                    id = w.ID,
                    name = w.Name
                }));
            }

            public async Task<IActionResult> OnPostCreateExportAsync(int id)
            {
                // Lấy thông tin đơn nhập kho
                var importRequest = await _context.ImportRequests
                    .Include(ir => ir.Details)
                    .FirstOrDefaultAsync(ir => ir.ID == id);
            
                if (importRequest == null)
                {
                    return NotFound();
                }
            
                // Tạo đơn xuất kho
                var exportRequest = new ExportRequest
                {
                    WarehouseId = importRequest.WarehouseId,
                    ExportDate = DateTime.Now,
                    Status = 1, // Trạng thái mặc định
                    Details = importRequest.Details.Select(d => new ExportRequestDetail
                    {
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                    }).ToList()
                };
            
                // Lưu đơn xuất kho vào cơ sở dữ liệu
                _context.ExportRequests.Add(exportRequest);
            
                // Cập nhật trạng thái đơn nhập kho
                importRequest.Status = 3; // Đã xuất kho
                await _context.SaveChangesAsync();
            
                return RedirectToPage("/Warehouse/warehouseDelivery");
            }
      


        }

} 