using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameShop.Domain.Entities;
using GameShop.Domain.ValueObjects;

namespace GameShop.Infrastructure.Data.EntityConfigurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Publisher)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasConversion(
                v => v.Value,
                v => Price.Create(v));

        builder.Property(p => p.GameConsoleId)
            .IsRequired();

        builder.HasOne(e => e.GameConsole)
               .WithMany(c => c.Games)
               .HasForeignKey(e => e.GameConsoleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
