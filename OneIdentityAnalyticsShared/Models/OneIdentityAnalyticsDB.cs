
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace OneIdentityAnalyticsShared.Models
{
    public class PowerBiTenant
    {
        [Key]
        public string CCC_Name { get; set; }
        public string CCC_WorkspaceId { get; set; }
        public string CCC_WorkspaceUrl { get; set; }
        public string CCC_DatabaseServer { get; set; }
        public string CCC_DatabaseName { get; set; }
        public string CCC_DatabaseUserName { get; set; }
        public string CCC_DatabaseUserPassword { get; set; }
        public string UID_CCCTenants { get; set; }
        public string XObjectKey { get; set; }
    }
    public class PowerBITenantInTenantsHasPersons
    {
        [Key]
        public string UID_CCCTenantsHasPersons;
        public PowerBiTenant tenant;
    }
    public class TenantsHasPersons
    {
        [Key]
        public string UID_CCCTenantsHasPersons { get; set; }
        public string CCC_UIDTenant { get; set; }
        public string CCC_UIDPerson { get; set; }
        public string XObjectKey { get; set; }
        
        
    }
  
    public class Person
    {
        [Key] 
        public string UID_Person { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CentralAccount { get; set; }
    }
    public class TenantsAssignedToPerson 
    {
        [Key]
        public List<PowerBITenantInTenantsHasPersons> Tenants { get; set; }
        public Person Person { get; set; }

    }
    public class PersonInAERole
    {
        [Key]
        public  string UID_Person { get; set; }
        public string UID_AERole{ get; set; }
        public string XObjectKey{ get; set; }

    }

    public class User
    {
        [Key]
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public bool CanEdit { get; set; }
        public bool CanCreate { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }
        public string TenantName { get; set; }
    }

    public class ActivityLogEntry
    {
        public int Id { get; set; }
        public string CorrelationId { get; set; }
        public string EmbedTokenId { get; set; }
        public string LoginId { get; set; }
        public string Activity { get; set; }
        public string Tenant { get; set; }
        public string WorkspaceId { get; set; }
        public string Dataset { get; set; }
        public string DatasetId { get; set; }
        public string Report { get; set; }
        public string ReportId { get; set; }
        public string OriginalReportId { get; set; }
        public int? LoadDuration { get; set; }
        public int? RenderDuration { get; set; }
        public DateTime Created { get; set; }
    }
    public class OneIdentityAnalyticsDB : DbContext
    {
        public OneIdentityAnalyticsDB(DbContextOptions<OneIdentityAnalyticsDB> options) : base(options) { }

        public DbSet<PowerBiTenant> CCCTenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonInAERole> PersonInAERole { get; set; }
       public DbSet<ActivityLogEntry> ActivityLog { get; set; }
       public DbSet<TenantsHasPersons> CCCTenantsHasPersons { get; set; }
       



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<OneIdentityAnalyticsDB>
        {
            public OneIdentityAnalyticsDB CreateDbContext(string[] args)
            {
                string configFilePath = @Directory.GetCurrentDirectory() + "/../OneIdentityAnalytics/appsettings.json";
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(configFilePath).Build();
                var builder = new DbContextOptionsBuilder<OneIdentityAnalyticsDB>();
                builder.UseSqlServer(configuration["AppOwnsDataDB:ConnectString"]);
                return new OneIdentityAnalyticsDB(builder.Options);
            }

        }
    }
}
