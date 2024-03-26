namespace Programmering_Eksamen.Data.Entities
{
    public class Product
    {
        public int Id { get; set; } //Sættes automatisk af databasen
        public int DBID { get; set; } //den ID der skal hentes i API'en
        public double Cost { get; set; } //hvor meget den skal koste
        public string Name { get; set; }
        public string imgURL { get; set; }
        public string Description { get; set; }
		public int Amount { get; set; } //Hvor mange af dem der er
	}
}
