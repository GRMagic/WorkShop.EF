﻿using WorkShop.EF.Models;

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