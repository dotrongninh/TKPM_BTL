using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    [Table("tblKho")]
    public class Warehouse
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Column("sTenkho")]
        [StringLength(225)]
        public string Name { get; set; }

        [Required]
        [Column("sDiachi")]
        [StringLength(255)]
        public string Address { get; set; }

        [Column("iSucchua")]
        public int Capacity { get; set; }

        [Column("fDientichkho")]
        public double Area { get; set; }

        [Column("iTinhtrangkho")]
        public bool IsActive { get; set; }

        [Column("iQuanly")]
        public int? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }
    }

    [Table("tblNhanvien")]
    public class Employee
    {
        [Key]
        public int ID { get; set; }

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
        public string HomeTown { get; set; }

        [Column("dNgayvaolam")]
        public DateTime JoinDate { get; set; }

        [Column("sSDT")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Column("sCCCD")]
        [StringLength(20)]
        public string IdCard { get; set; }

        [Column("bTrangthai")]
        public bool Status { get; set; }

        [Column("quyen")]
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }

    [Table("tblQuyen")]
    public class Role
    {
        [Key]
        public int ID { get; set; }

        [Column("sTenquyen")]
        [StringLength(225)]
        public string Name { get; set; }
    }
}