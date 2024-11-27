using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.Property(c => c.Nome).HasMaxLength(50);
        builder.HasIndex(c => c.Nome).IsUnique();

        builder.HasMany(c => c.Professores)
               .WithMany(p => p.Cursos)
               .UsingEntity(
                    "CursoProfessor",
                    l => l.HasOne(typeof(Professor)).WithMany().HasForeignKey("ProfessorId").HasPrincipalKey(nameof(Professor.Id)),
                    r => r.HasOne(typeof(Curso)).WithMany().HasForeignKey("CursoId").HasPrincipalKey(nameof(Curso.Id)),
                    j => j.HasKey("CursoId", "ProfessorId")
                );
    }
}
