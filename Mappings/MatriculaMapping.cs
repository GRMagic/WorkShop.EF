using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class MatriculaMapping : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.Property(m => m.Data);

        builder.HasOne(m => m.Estudante).WithMany(e => e.Matriculas).HasForeignKey(m => m.EstudanteId);
        builder.HasOne(m => m.Curso).WithMany(e => e.Matriculas).HasForeignKey(m => m.CursoId);
    }
}
