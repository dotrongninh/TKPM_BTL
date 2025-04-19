using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL_Ninh_Kho.Modules.Warehouse.Models
{
    [Table("tblNhacungcap")]
    public class Supplier
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("sTenNCC")]
        [Required]
        public string Name { get; set; }

        [Column("sDiachi")]
        public string Address { get; set; }

        [Column("sSDT")]
        public string Phone { get; set; }

        [Column("sEmail")]
        public string Email { get; set; }
    }
} 