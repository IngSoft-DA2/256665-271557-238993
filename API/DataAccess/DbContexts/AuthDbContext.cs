using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbContexts;

public class AuthDbContext : DbContext
{

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<Session> Sessions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>().ToTable("Administrators");
        modelBuilder.Entity<Manager>().ToTable("Managers");
        modelBuilder.Entity<RequestHandler>().ToTable("RequestHandlers");
        modelBuilder.Entity<Owner>().ToTable("Owners");

        modelBuilder.Entity<Administrator>()
            .Property(admin => admin.Id)
            .IsRequired();

        modelBuilder.Entity<Manager>()
            .Property(manager => manager.Id)
            .IsRequired();

        modelBuilder.Entity<RequestHandler>()
            .Property(reqHandler => reqHandler.Id)
            .IsRequired();

        modelBuilder.Entity<Owner>()
            .Property(owner => owner.Id)
            .IsRequired();

    }


}

