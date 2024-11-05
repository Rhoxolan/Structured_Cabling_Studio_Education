using Microsoft.AspNetCore.Identity;

namespace StructuredCablingStudio.Entities
{
	public class User : IdentityUser
	{
		public ICollection<CablingConfiguration> Configurations { get; set; } = default!;
	}
}
