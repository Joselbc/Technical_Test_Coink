using System.Reflection;
using UserContactRegistration.Application.Attributes;
using UserContactRegistration.Application.Exceptions;
using UserContactRegistration.Domain.Extensions;
using UserContactRegistration.Infrastructure.Extensions;
using UserContactRegistration.Infrastructure.PostgreSQL.Settings;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.Configure<PostgreSettings>((IConfiguration)StaticAttributes.GetPostgrelAttributes(config));

// Add services to the container.
builder.Services.AddDomainServices().AddPersistence();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.Load("UserContactRegistration.Infrastructure"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();