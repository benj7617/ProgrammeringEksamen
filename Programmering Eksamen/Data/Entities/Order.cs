namespace Programmering_Eksamen.Data.Entities
{
    public class Order
    {
        public int Id { get; set; } //ID til brug som foreign key for joining table
        public DateTime CreatedDate { get; set; } //Dato hvorpå ordren blev lavet 
        public ICollection<OrderProducts> Items { get; set; } //Icollection er en fleksibel kollektions format, der virker lidt som lister, men også kan håndtere andre data. Ikke helt nødvendg hvis joining tables bruges på en hvis måde


    }
}
