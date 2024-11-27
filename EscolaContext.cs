namespace WorkShop.EF;

public class EscolaContext : DbContext
{
    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(@"User Id=workshop_ef_gustavo;Password=teste123;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(Host=172.16.25.135)(Port=1521))(CONNECT_DATA=(SERVICE_NAME=dev19c31.supero.com.br)))");
        
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error);
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EscolaContext).Assembly);
    }

    public DbSet<Estudante> Estudantes { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Professor> Professores { get; set; }

}
