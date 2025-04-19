using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    [Table("tblNhanvien")]
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column("sTenNV")]
        [StringLength(225)]
        public string Name { get; set; }

        [Column("dNgaySinh")]
        public DateTime? DateOfBirth { get; set; }

        [Column("sGioitinh")]
        [StringLength(10)]
        public string Gender { get; set; }

        [Column("sQuequan")]
        [StringLength(255)]
        public string Hometown { get; set; }

        [Column("dNgayvaolam")]
        public DateTime? StartDate { get; set; }

        [Column("sSDT")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Column("sCCCD")]
        [StringLength(20)]
        public string IDCard { get; set; }

        [Column("bTrangthai")]
        public bool Status { get; set; }

        [Column("quyen")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
} 