using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Persistence.Configurations;

public class DiscussionsConfiguration : IEntityTypeConfiguration<Discussion>
{
  public void Configure(EntityTypeBuilder<Discussion> builder)
  {
    builder.ToTable("Discussions").HasKey(d => d.Id);
    builder.HasMany(d => d.Comments).WithOne(c => c.Discussion);
    builder.HasOne(d => d.Creator).WithMany(c => c.Discussions);
  }
}