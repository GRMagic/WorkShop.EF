using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.Property(c => c.Nome).HasMaxLength(50);
        builder.HasIndex(c => c.Nome).IsUnique();
    }
}
