using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CourseProgressConfiguration : IEntityTypeConfiguration<CourseProgress>
{
    public void Configure(EntityTypeBuilder<CourseProgress> builder)
    {
        builder.HasKey(p => new {p.UserId, p.CourseId});

        builder.HasOne(c => c.User)
            .WithMany(u => u.CourseProgresses)
            .HasForeignKey(c => c.UserId);
    }
}