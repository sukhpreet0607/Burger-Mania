namespace BurgerManiaServer.Utilities
{
    public class RemoveOrderParam
    {
        public string Burger { get; set; }
        public string Category { get; set; }
        public int UserId { get; set; }

        public override string ToString()
        {
            return $"{Burger}, {Category}, {UserId}";
        }
    }
}
