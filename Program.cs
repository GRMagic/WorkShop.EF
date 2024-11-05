using var context = new EscolaContext();

context.Database.EnsureCreated();

context.Database.Migrate();

// Criação de um novo estudante
var novoEstudante = new Estudante()
{
    Nome = "Fulano",
    Sobrenome = "De Tal",
};

context.Estudantes.Add(novoEstudante);

context.SaveChanges();

// Listagem de estudantes

var estudantes = context.Estudantes.ToList();

foreach (var estudante in estudantes)
    Console.WriteLine($"Id: {estudante.Id}, Nome: {estudante.Nome} {estudante.Sobrenome}");

// Atualização de um estudante

estudantes[0].Nome = "Ciclano";

context.SaveChanges();

// Busca de um estudante

var ciclano = context.Estudantes.First(e => e.Nome == "Ciclano");

Console.WriteLine($"Id: {ciclano.Id}, Nome: {ciclano.Nome} {ciclano.Sobrenome}");

// Remoção de um estudante

context.Estudantes.Remove(ciclano);

context.SaveChanges();


