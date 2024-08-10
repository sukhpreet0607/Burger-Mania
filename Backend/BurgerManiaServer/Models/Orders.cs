using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerManiaServer.Models
{
    [Table("Orders")]
    public class Orders
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string Burger {  get; set; }
        [Required]
        public string Category { get; set; }
        public int? Price { get; set; }
        public int Quantity { get; set; }
        public int? TotalPrice { get; set; }
        public int UserId { get; set; }
        public bool IsCheckout { get; set; }

        public override string ToString()
        {
            return $"UserId: ${UserId}, ItemId: {ItemId}, Burger: {Burger}, Category: {Category}, Price: {Price}, Quantity: {Quantity},TotalPrice: {TotalPrice}, IsCheckout: ${IsCheckout}";
        }
    }
}