using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using GraphQL.Server;
using Zerno.Data;
using Zerno.GraphQL.Schemas;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GrainContext>(options => options.UseSqlite(
        builder.Configuration.GetConnectionString("sqlite")
    ));
builder.Services.AddScoped<IGrainStorage, GrainService>();
builder.Services.AddScoped<ISchema, GrainSchema>();
builder.Services.AddGraphQL(options => { options.EnableMetrics = true; }).AddSystemTextJson();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseGraphQLAltair();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
