using Microsoft.AspNetCore.Mvc;
using BTL_Ninh_Kho.Modules.Warehouse.Services;
using BTL_Ninh_Kho.Modules.Warehouse.Models;

namespace BTL_Ninh_Kho.Controllers.Api
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            var result = await _warehouseService.UpdateWarehouseStatusAsync(id, request.IsActive);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _warehouseService.DeleteWarehouseAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }
    
        [HttpPut("{id}/update-status")]
        public async Task<IActionResult> UpdateImportStatus(int id, [FromBody] UpdateImportStatusRequest request)
        {
            var result = await _warehouseService.UpdateImportStatusAsync(id, request.Status);
            if (!result)
                return NotFound();

            return Ok();
        }

        public class UpdateImportStatusRequest
        {
            public int Status { get; set; }
        }


        public class UpdateStatusRequest
        {
            public bool IsActive { get; set; }
        }
    }
} 