using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PedagogyWorld.Data
{
    public class Context : DbContext
   {
      public Context()
         : base("PedagogyWorldConnectionString")
      {
         //Database.SetInitializer(new Initializer());
         //Database.SetInitializer(new DropCreateDatabaseAlways<Context>());
      }

      public DbSet<District> Districts { get; set; }
      public DbSet<File> Files { get; set; }
      public DbSet<FileType> FileTypes { get; set; }
      public DbSet<Grade> Grades { get; set; }
      public DbSet<Outcome> Outcomes { get; set; }
      public DbSet<School> Schools { get; set; }
      public DbSet<State> States { get; set; }
      public DbSet<Subject> Subjects { get; set; }
      public DbSet<Unit> Units { get; set; }
      public DbSet<UserProfile> UserProfiles { get; set; }
      public DbSet<TeachingDate> TeachingDates { get; set; }
      public DbSet<Standard> Standards { get; set; }

      protected override void OnModelCreating(DbModelBuilder modelBuilder)
      {
          modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
      }
   }

    //public class Initializer : DropCreateDatabaseAlways<Context>
    public class Initializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);
        }
    }
}