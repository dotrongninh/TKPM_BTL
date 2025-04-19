using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    [Table("tblHanghoa")]
    public class Product
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("sTenhanghoa")]
        [Required]
        public string Name { get; set; }

        [Column("iLoaihang")]
        public int CategoryId { get; set; }

        [Column("iDonvitinh")]
        public int UnitId { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
    }

    [Table("tblDonvitinh")]
    public class Unit
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("sTendonvitinh")]
        [Required]
        public string Name { get; set; }
    }
} 