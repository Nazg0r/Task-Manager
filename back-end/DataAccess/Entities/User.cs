
using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
	[Table("users")]
	public class User : BaseEntity
	{
		[Column("name")] public string Name { get; set; }
		[Column("surname")] public string Surname { get; set; }
	}
}
