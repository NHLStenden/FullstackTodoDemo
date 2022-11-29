using Bogus;
using Slugify;

namespace TodoBackend.Entities;

public class DbSeed
{
    public static void Seed(TodoContext db)
    {
        int numCategories = 10;
        int numOfTodos = 50;
        
        var slugHelper = new SlugHelper();
        
        Randomizer.Seed = new Random(8675309);
        Faker<Category> categoryFaker = new Faker<Category>()
            .RuleFor(x => x.Description, faker => $"Category {faker.IndexFaker}")
            .RuleFor(x => x.Slug, (faker, category) => slugHelper.GenerateSlug(category.Description));

        var categories = categoryFaker.Generate(numCategories);

        Faker<Todo> todoFaker = new Faker<Todo>()
            .RuleFor(x => x.Category, faker => categories[faker.Random.Int(0, numCategories-1)])
            .RuleFor(x => x.Completed, faker => faker.Random.Bool())
            .RuleFor(x => x.Description, faker => faker.Random.Words(faker.Random.Int(1, 5)));

        var todos = todoFaker.Generate(numOfTodos);
        db.Todos.AddRange(todos);
        db.SaveChanges();
    }
}