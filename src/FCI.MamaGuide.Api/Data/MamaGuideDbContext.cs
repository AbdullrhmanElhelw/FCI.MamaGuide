using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FCI.MamaGuide.Api.Data;

public sealed class MamaGuideDbContext : IdentityDbContext<BaseIdentityEntity, IdentityRole<Guid>, Guid>
{
    public MamaGuideDbContext(DbContextOptions<MamaGuideDbContext> options)
        : base(options)
    {
    }

    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Hospital> Hospitals => Set<Hospital>();
    public DbSet<Article> Articles => Set<Article>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(MamaGuideDbContext).Assembly);
    }
}