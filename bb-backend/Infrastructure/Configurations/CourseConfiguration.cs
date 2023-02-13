using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Exam)
            .WithOne(c => c.Course)
            .HasForeignKey<Course>(c => c.ExamId)
            .IsRequired(); // exam is always present for a course

        builder.HasMany(c => c.Lessons)
            .WithOne(l => l.Course)
            .HasForeignKey(l => l.CourseId);
    }
}