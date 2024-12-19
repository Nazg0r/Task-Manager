using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
	public class BaseEntity
	{
		[Column("id")] public int Id { get; set; }
	}
}
