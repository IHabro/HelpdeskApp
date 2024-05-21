using DataLayer.Areas.Identity.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataLayer.DbContexts;

public class IdentityDbContext : IdentityDbContext<HelpdeskUser>
{
    public DbSet<EscalationLevel> EscalationLevels { get; set; }
    public DbSet<EscalationAction> EscalationActions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectRole> ProjectRoles { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<UserToProjectToRole>().HasKey(jt => new { jt.User_Fk, jt.Project_Fk, jt.Role_Fk});
        builder.Entity<UserToProjectToRole>().HasOne(jt => jt.User).WithMany(u => u.RolesInProjects).HasForeignKey(jt => jt.User_Fk);
        builder.Entity<UserToProjectToRole>().HasOne(jt => jt.Project).WithMany(p => p.UsersAndRoles).HasForeignKey(jt => jt.Project_Fk);
        builder.Entity<UserToProjectToRole>().HasOne(jt => jt.Role).WithMany(r => r.UsersInProjects).HasForeignKey(jt => jt.Role_Fk);
        builder.Entity<UserToProjectToRole>().Property(jt => jt.GrantedOn).HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Entity<ActionToProjectToRole>().HasKey(jt => new { jt.Action_Fk, jt.Project_Fk, jt.Role_Fk });
        builder.Entity<ActionToProjectToRole>().HasOne(jt => jt.Action).WithMany(u => u.ActionOnProjectAndRole).HasForeignKey(jt => jt.Action_Fk);
        builder.Entity<ActionToProjectToRole>().HasOne(jt => jt.Project).WithMany(p => p.ActionOnRoleInProject).HasForeignKey(jt => jt.Project_Fk);
        builder.Entity<ActionToProjectToRole>().HasOne(jt => jt.Role).WithMany(r => r.ActionForProjectAndRole).HasForeignKey(jt => jt.Role_Fk);

    }
}
