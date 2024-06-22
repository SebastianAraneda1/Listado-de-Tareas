using ListadoDeTareas.Dal;
using ListadoDeTareas.IServices;
using ListadoDeTareas.ServiceImpl;
using ListadoDeTareas.Utils;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<PrioridadInterface, PrioridadImpl>();
builder.Services.AddScoped<TareaInterface, TareaImpl>();
builder.Services.AddScoped<DalImpl, DalImpl>();
builder.Services.AddScoped<ValidacionFeriado>();

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

app.UseCors(options =>
options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
