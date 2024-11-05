namespace WorkShop.EF;

public class EscolaContext : DbContext
{
    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseOracle(@"User Id=workshop_ef_gustavo;Password=teste123;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(Host=172.16.25.135)(Port=1521))(CONNECT_DATA=(SERVICE_NAME=dev19c31.supero.com.br)))");
    }

    public DbSet<Estudante> Estudantes { get; set; }

}
