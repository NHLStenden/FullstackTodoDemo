using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TodoBackend;
using TodoBackend.Entities;
using TodoBackend.Repositories;
using TodoBackend.Requests;
using TodoBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<IMyLogger, MyDbLogger>();
builder.Services.AddSingleton<IMyLogger, CombineLogger>(_ => 
    new CombineLogger(new MyConsoleLogger(), new MyDbLogger())
);

builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ISlugService, SlugService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=TodoLesDemo;")
);
// builder.Services.AddSingleton<>()

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryRequestValidator>();
// builder.Services.AddScoped<IValidator<CategoryRequest>, CategoryRequestValidator>();

// var angularPolicy = "AngularSpaPolicy";
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            //.WithOrigins("http://localhost:4200")
            .AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var db = service.GetService<TodoContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    DbSeed.Seed(db); //vult database met dummy data
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();