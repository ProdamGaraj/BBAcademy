using System.Data.Entity;

namespace Backend.Models
{
	public class Initializer
		: CreateDatabaseIfNotExists<BBAcademyDb>
	{
		protected override void Seed(BBAcademyDb context)
		{

			base.Seed(context);
		}
	}
}
