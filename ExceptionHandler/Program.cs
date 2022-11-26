using ExceptionHandler.Extensions;
using ExceptionHandler.Middlewares;
using Serilog;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog
    (
        loggers:new WriteTo[]
        {
            WriteTo.File,
            WriteTo.Console,
            WriteTo.DataBase
        },
        connectionString: "Server=(localdb)\\MSSQLLocalDB;Database=SerilogTestDb;Trusted_Connection=True;"
    );

// Add services to the container.

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

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
