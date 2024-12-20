
using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("users")]
	public class User : BaseEntity
	{
		[Column("name")] public string Name { get; set; }
		[Column("surname")] public string Surname { get; set; }

		public override bool Equals(object? obj)
		{
			return obj is User user &&
			user.Id == Id &&
			user.Name == Name &&
			user.Surname == Surname;
		}

		public override int GetHashCode() 
		{
			return HashCode.Combine(Id);
		}
	}
}
