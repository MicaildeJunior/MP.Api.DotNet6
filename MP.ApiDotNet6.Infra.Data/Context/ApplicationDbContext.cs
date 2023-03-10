using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	// Essa entidade será uma tabela no banco
	public DbSet<Person> People { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<Purchase> Purchases { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<PersonImage> PersonImages { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}
}
