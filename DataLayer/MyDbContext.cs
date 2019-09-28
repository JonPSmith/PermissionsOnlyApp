// Copyright (c) 2019 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using DataLayer.ExtraAuthClasses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MyDbContext : DbContext
    {
        //your DbSet<T> properties go here

        public DbSet<UserToRole> UserToRoles { get; set; }
        public DbSet<RoleToPermissions> RolesToPermissions { get; set; }
        public DbSet<ModulesForUser> ModulesForUsers { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Your configuration code will go here

            //ExtraAuthClasses configurations
            modelBuilder.Entity<UserToRole>().HasKey(x => new { x.UserId, x.RoleName });

            modelBuilder.Entity<RoleToPermissions>()
                .Property("_permissionsInRole")
                .HasColumnName("PermissionsInRole");
        }
    }
}