using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Orderdate { get; set; }

        // Foreign key

        [Column("ProductId")]
        public int ProductId { get; set; }

        //Navigation property

        public Product Product { get; set; }
    }
}
