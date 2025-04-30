using ConcesionarioApi.Data;
using ConcesionarioApi.DTOs.Validators;
using ConcesionarioApi.Interfaces;
using ConcesionarioApi.Profiles;
using ConcesionarioApi.Repositories;
using ConcesionarioApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL y Ef
builder.Services.AddDbContext<ConcesionarioDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

//Repository y Service
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAutoRepository, AutoRepository>();
builder.Services.AddScoped<IAutoService, AutoService>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(AutoProfile).Assembly);

//Controllers
builder.Services
    .AddControllers()
    .AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<CreateAutoDtoValidator>());

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy
          .WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
