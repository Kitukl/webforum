using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Persistence.Configurations;

public class CoursesConfiguration : IEntityTypeConfiguration<Course>
{
  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.ToTable("Courses").HasKey(c => c.Id);
    builder.HasOne(c => c.Creator).WithMany(u => u.CreatedCourses);
    builder.HasMany(c => c.Students).WithMany(u => u.EnrolledCourses);
    builder.HasMany(c => c.Lessons).WithOne(l => l.Course);
  }
}