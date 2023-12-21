using ApiBlog.Data;
using ApiBlog.Mappers;
using ApiBlog.Repository;
using ApiBlog.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Configuramos la conexon sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql"));
});

//Agregar automapper
builder.Services.AddAutoMapper(typeof(BlogMapper));


// Add services to the container.

builder.Services.AddScoped<IPostRepositorio, PostRepositorio>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
