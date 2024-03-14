namespace Programmering_Eksamen.Data.Entities
{
	public class User
	{
		public int Id { get; set; } //autogenereret Id
		public string Name { get; set; } //brugeresn brugernavn
		public string password { get; set; } //brugerens password
		public string email {  get; set; } //brugerens email
		public ICollection<Order> Orders { get; set; } //en icollection af orders
	}
}
