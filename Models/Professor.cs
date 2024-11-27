namespace WorkShop.EF.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<Curso> Cursos { get; set; } = [];
    }
}
