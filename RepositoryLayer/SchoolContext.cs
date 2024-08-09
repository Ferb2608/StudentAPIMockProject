using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {

        }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder
            //    .Entity<Student>()
            //    .HasOne(s => s.Address)
            //    .WithOne()
            //    .HasForeignKey<Student>(s => s.Address)
            //    .OnDelete(DeleteBehavior.Cascade);
            var grade1 = new Grade
            {
                Id = 1,
                GradeValue = "10"
            };

            var grade2 = new Grade
            {
                Id = 2,
                GradeValue = "11"
            };

            var grade3 = new Grade
            {
                Id = 3,
                GradeValue = "12"
            };

            var address1 = new StudentAddress { Id = 1, Street = "30/4", City = "DN", PostalCode = "0000", Country = "VN", Province = "DN" };

            modelBuilder.Entity<Grade>().HasData(grade1, grade2, grade3);
            modelBuilder.Entity<StudentAddress>().HasData(address1);
            modelBuilder.Entity<Student>().HasData(new Student { Id = 1, FirstName = "Hieu", LastName = "Cao", Phone = "055555555", GradeId = grade1.Id, AddressId = address1.Id });
        }

    }


}
