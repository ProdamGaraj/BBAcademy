using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(x => x.UserId);
        
        builder.HasMany(o => o.Courses)
            .WithMany(c => c.Orders)
            .UsingEntity<OrderLine>(
                r => r
                    .HasOne(k => k.Course)
                    .WithMany(m => m.OrderLines)
                    .HasForeignKey(k => k.CourseId),
                l => l
                    .HasOne(k => k.Order)
                    .WithMany(m => m.OrderLines)
                    .HasForeignKey(k => k.OrderId),
                cfg => cfg
                    .HasKey(r => new {r.OrderId, r.CourseId})
            );
    }
}