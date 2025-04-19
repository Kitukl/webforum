using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Persistence.Configurations;

public class LessonsConfiguration : IEntityTypeConfiguration<Lesson>
{
  public void Configure(EntityTypeBuilder<Lesson> builder)
  {
    builder.ToTable("Lessons").HasKey(l => l.Id);
    builder.HasOne(l => l.Course).WithMany(c => c.Lessons);
  }
}