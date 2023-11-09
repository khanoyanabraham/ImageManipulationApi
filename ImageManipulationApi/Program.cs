using ImageManipulation.Application;
using ImageManipulation.Application.Configuration;
using ImageManipultaion.Proccesor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigurationBuilder configurationBuilder = new();

configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

configurationBuilder.Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterProcessors();
builder.Services.RegisterApplication();



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
