using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using TaskManagement.API.Data;
using TaskManagement.API.Mappings;
using TaskManagement.API.Repositories;
using TaskManagement.API.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding Logger
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/TaskManagementApi_Logs.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();

// Injecting this to upload Document
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injecting the DbContext and supplying the  connection string
builder.Services.AddDbContext<TaskManagementSystemDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagementSystemConnectionString")));

// Injecting the repositories
builder.Services.AddScoped<ITeamRepository, SQLTeamRepository>();
builder.Services.AddScoped<IEmpTaskRepository, SQLEmpTaskRepository>();
builder.Services.AddScoped<INoteRepository, SQLNoteRepository>();
builder.Services.AddScoped<IDocumentRepository, LocalDocumentRepository>();


// Injecting the AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

// To open static files using url
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Documents")),
    RequestPath = "/Documents"
});

app.MapControllers();

app.Run();
