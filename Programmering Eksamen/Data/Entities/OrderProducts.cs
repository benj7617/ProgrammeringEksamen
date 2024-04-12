namespace Programmering_Eksamen.Data.Entities
{
    public class OrderProducts
    {
        public int Id { get; set; } //nødvendig for at programmet virker, det SKAL hede Id
        public int Order { get; set; } //fremmednøglen for Order objektet
        public int Product { get; set; } //fremmednøglen for Product objektet

    }
}
