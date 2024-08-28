using GameShop.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GameShop.Domain.ValueObjects;

namespace GameShop.Infrastructure.Data.EntityConfigurations;

public class GameConsoleConfiguration : IEntityTypeConfiguration<GameConsole>
{
    public void Configure(EntityTypeBuilder<GameConsole> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Manufacturer)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasConversion(
                v => v.Value,
                v => Price.Create(v));
    }
}
