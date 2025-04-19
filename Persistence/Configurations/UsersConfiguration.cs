using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users").HasKey(u => u.Id);
    builder.HasMany(u => u.Discussions).WithOne(d => d.Creator);
  }
}