
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("users")]
	public class User
	{
		
		[Column("id")] public int Id { get; set; }
		[Column("name")] public string Name { get; set; }
		[Column("surname")] public string Surname { get; set; }
	}
}
