using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);

//System added services 

builder.Services.AddScoped<IProductRepository,ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var Scope=app.Services.CreateScope();
var services=Scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var logger=services.GetRequiredService<ILogger<Program>>();
 try
 {
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
 }
 catch (Exception ex)
 {
    logger.LogError(ex,"an error ouccred during migration");
 }

app.Run();
