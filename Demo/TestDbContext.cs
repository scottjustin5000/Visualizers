using System.Data.Entity;

namespace Demo
{
    public class TestDbContext:DbContext
    {
        static TestDbContext()
        {
            Database.SetInitializer<TestDbContext>(null);
        }
        public TestDbContext(string name):base(name){}
        public TestDbContext():base("name=dbstring"){}

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
        }
    }
}
