using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Persistence.Configurations;

public class CommentsConfiguration : IEntityTypeConfiguration<Comment>
{
  public void Configure(EntityTypeBuilder<Comment> builder)
  {
    builder.ToTable("Comments").HasKey(c => c.Id);
    builder.HasOne(c => c.Discussion).WithMany(d => d.Comments);
    builder.HasOne(c => c.Creator).WithMany();
  }
}