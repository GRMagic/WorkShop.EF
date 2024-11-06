namespace WorkShop.EF.Models
{
    public class Certificado 
    {
        public int Id { get; set; }
        public int MatriculaId { get; set; }
        public DateTime Conclusao { get; set; }

        public Matricula Matricula { get; set; }
    }
}
