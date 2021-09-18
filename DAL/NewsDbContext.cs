using Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL
{
    //Inherit DbContext class and use Entity Framework Code First Approach
    public class NewsDbContext:DbContext
    {
        //Create a Constructor and write a logic for Datbase created
        public NewsDbContext(DbContextOptions options):base(options){}
        /*
        This class should be used as DbContext to speak to database and should make the use of 
        Code First Approach. It should autogenerate the database based upon the model class in 
        your application
        */
        public DbSet<News> NewsList { get; set; }
        public DbSet<UserProfile> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        //Create a Dbset for News,USerProfile and Reminders

        /*Override OnModelCreating function to configure relationship between entities and initialize*/
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = builder.Entity<UserProfile>();
            user.HasKey(x => x.UserId);
            user.HasMany(x => x.NewsList).WithOne(x => x.User).HasForeignKey(x => x.CreatedBy).IsRequired();
            var reminder = builder.Entity<Reminder>();
            reminder.HasOne(x => x.News).WithOne(x => x.Reminder).HasForeignKey<Reminder>(x => x.NewsId).IsRequired();

        }
        //write a modelbuilder logic for Relationship between News and UserProfile in OnModelCreating Method
        //write a modelbuilder logic for Relationship between News and Reminder in OnModelCreating Method

    }
}
