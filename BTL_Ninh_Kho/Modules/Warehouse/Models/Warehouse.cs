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
}