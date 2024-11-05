namespace WorkShop.EF.Models
{
    public class Matricula
    {
        public int Id { get; set; }
        public int EstudanteId { get; set; }
        public int CursoId { get; set; }
        public DateTime Data { get; set; }

        public Estudante Estudante { get; set; }
        public Curso Curso { get; set; }
    }
}
