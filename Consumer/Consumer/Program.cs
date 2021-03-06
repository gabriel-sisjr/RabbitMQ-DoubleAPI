using Consumer;
using Domain.Interfaces;
using Domain.Models.Workers;
using Services;
using Services.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IPeopleService, PeopleService>();
if (Environment.GetEnvironmentVariable("AMBIENT") == "CONTAINER")
{
    var settings = builder.Configuration.GetSection("RabbitMqSettings").Get<RabbitSettings>();
    builder.Services.AddSingleton(x => RabbitSetup.CreateBus(settings!));
}
else
    builder.Services.AddSingleton(x => RabbitSetup.CreateBus("localhost"));

builder.Services.AddHostedService<Worker>(); // To Run as a service

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
