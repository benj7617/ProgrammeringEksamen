namespace Programmering_Eksamen.Data.Entities
{
    public class OrderProducts
    {
        public int Id { get; set; } //nødvendig for at programmet virker, det SKAL hede Id
        public Order Order { get; set; } //fremmednøglen for Order objektet
        public Product Product { get; set; } //fremmednøglen for Product objektet
        public int Quantity { get; set; } //hvor mange af dem der er købt i guess


    }
}
