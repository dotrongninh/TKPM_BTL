using System.ComponentModel.DataAnnotations;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    public class CreateWarehouseRequest
    {
        [Required(ErrorMessage = "Tên kho là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên kho không được vượt quá 100 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mã nhân viên quản lý là bắt buộc")]
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Sức chứa là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Sức chứa phải lớn hơn 0")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Diện tích là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Diện tích phải lớn hơn 0")]
        public double Area { get; set; }
    }
} 