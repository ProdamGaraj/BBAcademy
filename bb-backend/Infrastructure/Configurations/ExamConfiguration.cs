using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Questions)
            .WithOne(c => c.Exam)
            .HasForeignKey(c => c.ExamId);
    }
}

public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasOne(c => c.Question)
            .WithMany(c => c.AnswerOptions)
            .HasForeignKey(c => c.QuestionId);
    }
}