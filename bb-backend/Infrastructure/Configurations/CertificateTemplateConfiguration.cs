using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CertificateTemplateConfiguration : IEntityTypeConfiguration<CertificateTemplate>
{
    public void Configure(EntityTypeBuilder<CertificateTemplate> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Certificates)
            .WithOne(c => c.CertificateTemplate)
            .HasForeignKey(c => c.CertificateTemplateId);

        builder.HasMany(c => c.Courses)
            .WithOne(c => c.CertificateTemplate)
            .HasForeignKey(c => c.CertificateTemplateId);
    }
}