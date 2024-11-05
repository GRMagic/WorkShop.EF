using var context = new EscolaContext();

context.Database.EnsureCreated();

context.Database.Migrate();

