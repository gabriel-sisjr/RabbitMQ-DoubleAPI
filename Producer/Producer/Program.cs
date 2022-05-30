using Producer.Configuration.Extensions.Injections.Services;
using Producer.Configuration.Extensions.Injections.Workers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddServices()
    .AddWorkersSetup(builder.Configuration);

// Controllers
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
