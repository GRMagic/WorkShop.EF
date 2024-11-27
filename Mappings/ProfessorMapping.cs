using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class ProfessorMapping : IEntityTypeConfiguration<Professor>
{
    public void Configure(EntityTypeBuilder<Professor> builder)
    {
        builder.Property(p => p.Nome).HasMaxLength(50);
        builder.HasIndex(p => p.Nome).IsUnique();
    }
}