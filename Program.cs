using WorkShop.EF.Models;

using (var context = new EscolaContext())
{

    context.Database.EnsureDeleted();

    //context.Database.EnsureCreated();

    context.Database.Migrate();

    {
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
            Console.WriteLine($"{estudante.Id:00}: {estudante.Nome} {estudante.Sobrenome}");

        // Atualização de um estudante

        estudantes[0].Nome = "Sicrano";

        context.SaveChanges();

        // Busca de um estudante

        var sicrano = context.Estudantes.First(e => e.Nome == "Sicrano");

        Console.WriteLine($"Id: {sicrano.Id}, Nome: {sicrano.Nome} {sicrano.Sobrenome}");

        // Remoção de um estudante

        context.Estudantes.Remove(sicrano);

        context.SaveChanges();
    }

    // Inclusão de um estudante duplicado
    //try
    //{
    //    var beltrano = new Estudante()
    //    {
    //        Nome = "Beltrano",
    //        Sobrenome = "De Tal", 
    //    };
    //    context.Estudantes.Add(beltrano);
    //    var duplicado = new Estudante()
    //    {
    //        Nome = "Beltrano",
    //        Sobrenome = "De Tal",
    //    };
    //    context.Estudantes.Add(duplicado);
    //    context.SaveChanges();
    //}
    //catch (Exception ex)
    //{
    //    Console.WriteLine(ex.Message);
    //}

    // Exemplo inclusão de várias entidades usando um comando só
    {
        var curso = new Curso()
        {
            Nome = "Workshop EF Core"
        };

        var renato = new Estudante()
        {
            Nome = "Renato",
            Sobrenome = "Russo"
        };

        var matricula = new Matricula()
        {
            Estudante = renato,
            Curso = curso,
            Data = DateTime.Now
        };

        // Adiciona o curso, o estudante e a matrícula em um comando só

        context.Matriculas.Add(matricula);

        context.SaveChanges();
    }

    // Exemplo de consulta com join
    {
        var matriculas = context.Matriculas
            .Include(m => m.Estudante)
            .Include(m => m.Curso)
            .OrderBy(m => m.Curso.Nome)
            .ThenBy(m => m.Estudante.Nome)
            .ToList();

        const int largura = -40;
        Console.WriteLine($"\n{"Curso",largura} : Aluno");
        foreach (var matricula in matriculas)
            Console.WriteLine($"{matricula.Curso.Nome,largura} : {matricula.Estudante.Nome}");

    }

    {
        // Inserção de matriculas
        var random = new Random(123); // Deterministico!
        var cursosId = context.Cursos.Select(c => c.Id).ToArray();
        var estudantesId = context.Estudantes.Select(e => e.Id).ToArray();
        var matriculas = estudantesId.Select(estudantesId => new Matricula()
        {
            EstudanteId = estudantesId,
            CursoId = cursosId[random.Next(cursosId.Length)],
            Data = DateTime.Today.AddDays(-random.Next(5,10))
        }).ToArray();

        context.AddRange(matriculas);
        context.SaveChanges();

        // Inserção de certificados
        for (var i = 0; i < matriculas.Length; i++)
        {
            if (random.Next(100) < 30)
            {
                var certificado = new Certificado
                {
                    MatriculaId = matriculas[i].Id,
                    Conclusao = DateTime.Today.AddDays(random.Next(1, 3))
                };
                context.Add(certificado);
            }
        }
        var salvas = context.SaveChanges();
        Console.WriteLine($"Foram salvos {salvas} certificados.");

        // Inserindo alguns professores
        {
            var professores = new[]
            {
                new Professor { Nome = "João" },
                new Professor { Nome = "Maria" },
                new Professor { Nome = "José" },
            };
            context.Professores.AddRange(professores);
            var salvos = context.SaveChanges();
            Console.WriteLine($"Foram salvos {salvos} professores.");
        }
    }
}

using (var context = new EscolaContext())
{
    var query = context.Matriculas
        .Where(m => m.Certificado != null)
        .OrderByDescending(m => m.Certificado.Conclusao)
        .ThenByDescending(m => m.Data)
        .ThenBy(m => m.Estudante.Nome)
        .Select(m => new
        {
            curso = m.Curso.Nome,
            aluno = m.Estudante.Nome + " " + m.Estudante.Sobrenome,
            matricula = m.Data,
            conclusao = (DateTime?)m.Certificado!.Conclusao,
        });

    var sql = query.ToQueryString();
    Console.WriteLine(sql);

    var resultado = query.ToList();

    foreach (var item in resultado)
        Console.WriteLine($"{item.curso,-40} : {item.aluno, -20} : {item.matricula:M} : {item.conclusao:M}");
}

using (var context = new EscolaContext())
{
    var query = context.Estudantes
        //.Where(e => e.Nome.Contains('a')) // Funciona em memória, mas o EF não sabe como traduzir
        .Where(e => e.Nome.Contains("a") && e.Sobrenome.StartsWith("S")) // O EF sabe como traduzir para SQL
        .OrderBy(e => e.Nome)
        .Select(e => e.Nome);

    var sql = query.ToQueryString();
    Console.WriteLine(sql);

    var nomes = query.ToList();

    foreach (var nome in nomes)
        Console.WriteLine(nome);
}

using (var context = new EscolaContext())
{
    var query = context.Estudantes
        .Where(e => EF.Functions.Like(e.Nome.ToUpper(), "M%") && e.Nome.Length > 5)
        .OrderBy(e => e.Nome)
        .Select(e => e.Nome);

    var sql = query.ToQueryString();
    Console.WriteLine(sql);

    var nomes = query.ToList();

    foreach (var nome in nomes)
        Console.WriteLine(nome);
}

// Fazendo parte da expressão rodar no banco e parte em memória
using (var context = new EscolaContext())
{
    var noBanco = context.Estudantes.Select(e => new 
    {
        NomeCompleto = e.Nome + " " + e.Sobrenome 
    }).ToList();

    var naMemoria = context.Estudantes.ToList().Select(e => new
    {
        NomeCompleto = e.Nome + " " + e.Sobrenome
    }).ToList();

    var parteNoBancoParteNaMemoria = context.Estudantes.Select(e => new
    {
        NomeCompleto = e.Nome + " " + e.Sobrenome
    }).ToList().Select(e => new
    {
        NomeCompleto = Maiusculo(e.NomeCompleto)
    }).ToList();

    static string Maiusculo(string nome) => nome.ToUpper();
}


// Atribuições NxN
using(var context = new EscolaContext())
{
    var curso1 = context.Cursos.Find(1)!;
    var professor1 = context.Professores.Find(1)!;
    curso1.Professores.Add(professor1);
    context.SaveChanges();

    var curso2 = context.Cursos.Find(2)!;
    var professor2 = context.Professores.Find(2)!;

    professor2.Cursos.Add(curso1);
    professor2.Cursos.Add(curso2);

    context.SaveChanges();

    curso1 = context.Cursos.Include(c => c.Professores).First(c => c.Id == 1);
    professor1 = curso1.Professores.First(p => p.Id == 1);
    curso1.Professores.Remove(professor1);

    context.SaveChanges();

    var curso3 = context.Cursos.Include(c => c.Professores).First(c => c.Id == 3);
    var professor3 = context.Professores.First(p => p.Id == 3);
    curso3.Professores.Add(professor3);

    context.SaveChanges();

    Console.WriteLine(new string('-',80));
    Console.WriteLine("\nAlunos do professor 2:\n");

    var alunosProfessor2 = context.Professores.Where(p => p.Id == 2)
                                              .SelectMany(p => p.Cursos)
                                              .SelectMany(c => c.Matriculas)
                                              .Select(m => m.Estudante.Nome);

    Console.WriteLine(alunosProfessor2.ToQueryString());
    Console.WriteLine();
    foreach (var aluno in alunosProfessor2)
        Console.WriteLine(aluno);


    Console.WriteLine(new string('-', 80));
    Console.WriteLine("\nProfessores e seus alunos:\n");

    var alunosPorProfessor = context.Professores
                                    .Join(context.Cursos,
                                          p => p.Id,
                                          c => c.Professores.Select(p => p.Id).FirstOrDefault(),
                                          (p, c) => new { Professor = p, Curso = c })
                                    .Join(context.Matriculas,
                                         pc => pc.Curso.Id,
                                         m => m.CursoId,
                                         (pc, m) => new { pc.Professor, pc.Curso, m.Estudante })
                                    .Select(j => new
                                    {
                                        Professor = j.Professor.Nome,
                                        Aluno = j.Estudante.Nome + " " + j.Estudante.Sobrenome
                                    })
                                    .OrderBy(j => j.Professor)
                                    .ThenBy(j => j.Aluno);

    Console.WriteLine(alunosPorProfessor.ToQueryString());
    Console.WriteLine();
    foreach (var par in alunosPorProfessor)
        Console.WriteLine($"{par.Professor} -> {par.Aluno}");
}


// Exemplo de group by
using (var context = new EscolaContext())
{
    Console.WriteLine(new string('-', 80));

    var query = context.Matriculas
        .GroupBy(m => new
        {
            m.CursoId,
            m.Curso.Nome
        })
        .Select(g => new
        {
            Curso = g.Key.Nome,
            Quantidade = g.Count()
        });


    var sql = query.ToQueryString();
    Console.WriteLine(sql);
    var resultado = query.ToList();

    Console.WriteLine($"\n{"Nome do Curso",-40} : Quantidade de alunos");
    foreach (var item in resultado)
        Console.WriteLine($"{item.Curso, -40} : {item.Quantidade}");
}

using (var context = new EscolaContext())
{
    Console.WriteLine(new string('-', 80));
    Console.WriteLine("\nAlunos por professor:");

    var alunosDoProfessor = context.Professores
                                   .Join(context.Cursos,
                                         p => p.Id,
                                         c => c.Professores.Select(p => p.Id).FirstOrDefault(),
                                         (p, c) => new { Professor = p, Curso = c })
                                   .Join(context.Matriculas,
                                        pc => pc.Curso.Id,
                                        m => m.CursoId,
                                        (pc, m) => new { pc.Professor, pc.Curso, m.Estudante })
                                   .Select(j => new
                                   {
                                       Professor = j.Professor.Nome,
                                       Aluno = j.Estudante.Nome + " " + j.Estudante.Sobrenome
                                   })
                                   .OrderBy(j => j.Professor)
                                   .ThenBy(j => j.Aluno)
                                   .GroupBy(j => j.Professor)
                                   .ToList();

    foreach (var grupo in alunosDoProfessor)
    {
        Console.WriteLine($"\n{grupo.Key}");
        foreach (var item in grupo)
            Console.WriteLine($"\t{item.Aluno}");
    }
}

using (var context = new EscolaContext())
{
    Console.WriteLine(new string('-', 80));
    Console.WriteLine("\nAlunos por Curso:");

    var alunosCurso = context.Matriculas
        .Select(m => new 
        {
            Curso = m.Curso.Nome,
            Aluno = m.Estudante.Nome + " " + m.Estudante.Sobrenome
        })
        .OrderBy(m => m.Curso)
        .ThenBy(m => m.Aluno)
        .GroupBy(m => m.Curso)
        .ToList();

    foreach (var grupo in alunosCurso)
    {
        Console.WriteLine($"\n{grupo.Key}");
        foreach (var item in grupo)
            Console.WriteLine($"\t{item.Aluno}");
    }
}