using Microsoft.EntityFrameworkCore;
using Persistence.Configurations;
using Core.Entities;
public class Context(DbContextOptions<Context> options): DbContext(options)
{
  public DbSet<Comment> Comments { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<Discussion> Discussions { get; set; }
  public DbSet<Lesson> Lessons { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new CoursesConfiguration());
    modelBuilder.ApplyConfiguration(new CommentsConfiguration());
    modelBuilder.ApplyConfiguration(new DiscussionsConfiguration());
    modelBuilder.ApplyConfiguration(new LessonsConfiguration());
    modelBuilder.ApplyConfiguration(new UsersConfiguration());
  }
}