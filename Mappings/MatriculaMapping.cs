using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class MatriculaMapping : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.Property(e => e.Data);

        builder.HasOne(e => e.Estudante).WithMany(e => e.Matriculas).HasForeignKey(e => e.EstudanteId);
        builder.HasOne(e => e.Curso).WithMany(e => e.Matriculas).HasForeignKey(e => e.CursoId);
    }
}