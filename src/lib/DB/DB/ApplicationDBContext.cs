using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DB.DB;

public class ApplicationDbContext : DbContext
{
	private readonly string _connectionString = "Host=localhost;Port=5432;Database=testdbAYOS;Username=postgres;Password=postgres";
	public DbSet<Product> Products { get; set; } = null!;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public ApplicationDbContext()
	{
		Database.EnsureCreated();

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		// Used for Tests which use InMemoryDatabase (as not possible to use NpgSql and InMemoryDb simultaneously).
		// Might be good idea to re-write later.
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql(_connectionString);
		}
	}
}
