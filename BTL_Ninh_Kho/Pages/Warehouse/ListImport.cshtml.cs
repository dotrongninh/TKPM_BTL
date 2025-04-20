using Microsoft.AspNetCore.Mvc.RazorPages;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class ListImportModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ListImportModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<ImportRequest> ListImport { get; set; } = new List<ImportRequest>();

        public async Task OnGetAsync()
        {
            ListImport = await _context.ImportRequests
                .Include(i => i.Warehouse)
                .Include(i => i.Supplier)
                .OrderByDescending(i => i.ImportDate)
                .ToListAsync();
        }
    }
} 