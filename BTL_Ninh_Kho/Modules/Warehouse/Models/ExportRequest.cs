using System.ComponentModel.DataAnnotations;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    public class ExportRequest
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn kho")]
        public int WarehouseId { get; set; }
        
        public DateTime ExportDate { get; set; } = DateTime.Now;
        
        public int Status { get; set; }
        public Warehouse Warehouse { get; set; }

        public List<ExportRequestDetail> Details { get; set; } = new List<ExportRequestDetail>();
    }

    public class ExportRequestDetail
    {
        public int ID { get; set; }
        public int ExportRequestId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        
        // Navigation properties
        public Product Product { get; set; }
    }
} 