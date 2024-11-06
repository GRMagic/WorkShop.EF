using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorkShop.EF.Mappings;

internal class CertificadoMapping : IEntityTypeConfiguration<Certificado>
{
    public void Configure(EntityTypeBuilder<Certificado> builder)
    {
        builder.Property(c => c.Conclusao);

        builder.HasOne(c => c.Matricula).WithOne(m => m.Certificado).HasForeignKey<Certificado>(c => c.MatriculaId);
    }
}