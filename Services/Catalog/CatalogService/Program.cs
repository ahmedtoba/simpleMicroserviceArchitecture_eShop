using CatalogService.Infrastructure;
using CatalogService.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<CatalogContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("CatalogConn")));
}
else
{
    builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseInMemoryDatabase("Catalog"));
}


builder.Services.AddScoped<IProductRepo, ProductRepo>();
//add auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DbInitializer.Initialize(app, app.Environment.IsProduction());

app.Run();

