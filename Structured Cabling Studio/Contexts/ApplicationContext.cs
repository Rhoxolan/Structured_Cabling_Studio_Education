using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using StructuredCablingStudio.Entities;
using System.Text.Json;

namespace StructuredCablingStudio.Contexts
{
	public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext<User>(options)
	{
		public DbSet<CablingConfiguration> CablingConfigurations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CablingConfiguration>()
				.Property(c => c.Recommendations)
				.HasConversion(
					v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
					v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null!)!,
					new ValueComparer<Dictionary<string, string>>(
						(c1, c2) => c1!.SequenceEqual(c2!),
						c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
						c => c.ToDictionary(k => k.Key, v => v.Value)
						));
			base.OnModelCreating(modelBuilder);
		}
	}
}
