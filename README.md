## Todo voorbeeld

Het project bevat een fullstack webapplicatie.
In de directory ``TodoBackend`` kan je de backend vinden (ASP.NET Core) en in de directory ``front-end`` staat een Angular applicatie die verbinding maakt met de back-end voor het ophalen van de data. 

Je kan dit project openen in je favoriete IDE. Start dan de backend. En dan de front-end.

Back-end: deze kan je van uit de terminal starten met de volgende commando's:
```shell
cd TodoBackend
dotnet run
```
De front-end kan je starten door het volgende commandp's in de terminal uit te voeren:
```shell
cd front-end
ng serve
```



## Database

Voor de database is gebruik gemaakt van [PostgreSQL](http://www.postgresql.org).
De username = **postgres** en password = **postgres**. Dit zijn de standaard username & password. 
Uitteraard is het mogelijk om een andere database te gebruiken. Installeer dan de juiste driver (NuGet) voor Entity Framework Core en pas onderstaande 
code aan (zowel de connectionstring als the ``Use...()`` methoden, deze staat in ``Program.cs``).
```csharp
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=TodoLesDemo;")
);
```

Iedere keer als je de back-end start wordt de database opnieuw aangemaakt en gevuld.
Dit kan je eventueel uitzetten, zie Program.cs 
```csharp
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var db = service.GetService<TodoContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    DbSeed.Seed(db); //vult database met dummy data
}
```

## Opdrachten

- Een formulier voor het toevoegen van een categorie, eventueel in een eigen Angular component.
- Een formulier voor het updaten van een todo item, maak ook hiervoor een eigen component.
- Sorteren van de Categorieën en eventueel vanuit de front-end de volgorde aanpassen (asc of desc).
- Filter de lijst van todo’s op Name.
- Voeg een extra Property toe aan TodoResponse, b.v. de datum waarop het item is aangemaakt. Geeft dit ook weer in de front-end.