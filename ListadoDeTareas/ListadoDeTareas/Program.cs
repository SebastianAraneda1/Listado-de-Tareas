using ListadoDeTareas.Dal;
using ListadoDeTareas.IServices;
using ListadoDeTareas.ServiceImpl;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<PrioridadInterface, PrioridadImpl>();
builder.Services.AddScoped<TareaInterface, TareaImpl>();
builder.Services.AddScoped<DalImpl, DalImpl>();

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
