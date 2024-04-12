namespace Programmering_Eksamen.Data.Entities
{
    public class Order
    {
        public int Id { get; set; } //ID til brug som foreign key for joining table
        public string CreatedDate { get; set; } //Dato hvorpå ordren blev lavet 
        public int UserID {  get; set; } //ID på personen der har købt det, muligt ikke nødvendigt

    }
}
