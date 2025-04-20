using System.ComponentModel.DataAnnotations;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    public class ImportRequest
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn nhà cung cấp")]
        public int SupplierId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn kho")]
        public int WarehouseId { get; set; }
        
        public DateTime ImportDate { get; set; } = DateTime.Now;
        
        public int Status { get; set; } = 1; // Mặc định là Chờ duyệt

        public List<ImportRequestDetail> Details { get; set; } = new List<ImportRequestDetail>();

        // Navigation properties
        public Supplier Supplier { get; set; }
        public BTL_Ninh_Kho.Modules.Warehouse.Models.Warehouse Warehouse { get; set; }
    }

    public class ImportRequestDetail
    {
        public int ID { get; set; }
        public int ImportRequestId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = "Vui lòng nhập giá nhập")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập không hợp lệ")]
        public double ImportPrice { get; set; }

        // Navigation properties
        public Product Product { get; set; }
    }
} 