namespace WorkShop.EF.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }
    }
}
