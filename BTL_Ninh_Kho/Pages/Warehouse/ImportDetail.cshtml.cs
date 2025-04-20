using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BTL_Ninh_Kho.Data;
using BTL_Ninh_Kho.Modules.Warehouse.Models;
using Microsoft.EntityFrameworkCore;

namespace BTL_Ninh_Kho.Pages.Warehouse
{
    public class ImportDetailModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ImportDetailModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ImportRequest? ImportRequest { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ImportRequest = await _context.ImportRequests
                .Include(i => i.Supplier)
                .Include(i => i.Warehouse)
                .Include(i => i.Details)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.Unit)
                .FirstOrDefaultAsync(i => i.ID == id);

            if (ImportRequest == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
} 