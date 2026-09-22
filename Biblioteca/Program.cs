using Biblioteca;
using Biblioteca.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure
builder.Services.AddInfrastructureDI(builder.Configuration);

// Application
builder.Services.AddAppDI();

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();