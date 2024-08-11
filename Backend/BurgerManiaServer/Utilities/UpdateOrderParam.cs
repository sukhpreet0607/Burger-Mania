using BurgerManiaServer.Models;

namespace BurgerManiaServer.Utilities
{
    public class UpdateOrderParam
    {
        public string Burger {  get; set; }
        public string Category { get; set; }
        public Order UpdatedItem { get; set; }
    }
}
