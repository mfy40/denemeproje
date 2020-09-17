using kutuphane.database.Models;
using Microsoft.EntityFrameworkCore;

namespace kutuphane.database.Context
{
   public  class KutuphaneDbContext: DbContext
    {


        public KutuphaneDbContext(DbContextOptions<KutuphaneDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
  
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Bookshelf> Bookshelfs { get; set; }       
        public virtual DbSet<Writer> Writers { get; set; }
        public virtual DbSet<Student> Students { get; set; }    
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Faculty> Facultes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<BookWriter> BookWriters { get; set; }
        public virtual DbSet<Saloon> Saloons { get; set; }
        public virtual DbSet<StudentDepartment> StudentDepartments { get; set; }
        public virtual DbSet<Pricing> Pricings { get; set; }

    }



}
