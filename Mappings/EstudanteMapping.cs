using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class EstudanteMapping : IEntityTypeConfiguration<Estudante>
{
    public void Configure(EntityTypeBuilder<Estudante> builder)
    {
        builder.Property(e => e.Nome).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Sobrenome).IsRequired().HasMaxLength(100);
        builder.HasIndex(e => new { e.Nome, e.Sobrenome }).IsUnique();
    }
}
