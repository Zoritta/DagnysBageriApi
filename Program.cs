using DagnysBageriApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddControllers();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{

    var context = services.GetRequiredService<DataContext>();

    await context.Database.MigrateAsync();

    await Seed.LoadSuppliers(context);
    await Seed.LoadRawMaterials(context);
    await Seed.LoadSupplierMaterials(context);

}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}

app.MapControllers();

app.Run();
