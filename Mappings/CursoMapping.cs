using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.Property(e => e.Nome).HasMaxLength(50);
        builder.HasIndex(e => e.Nome).IsUnique();
    }
}
