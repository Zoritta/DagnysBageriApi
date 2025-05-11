using DagnysBageriApi;
using DagnysBageriApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

builder.Services.AddDbContext<DataContext>(options =>
{
    // options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
    options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);

});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5500")  // Allow frontend (Live Server)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{

    var context = services.GetRequiredService<DataContext>();

    await context.Database.EnsureDeletedAsync();
    await context.Database.MigrateAsync();

    await Seed.LoadSuppliers(context);
    await Seed.LoadRawMaterials(context);
    await Seed.LoadSupplierMaterials(context);
    await Seed.LoadProducts(context);
    await Seed.LoadCustomers(context);     
    await Seed.LoadOrders(context);        
    await Seed.LoadOrderItems(context);

}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}

app.UseCors();

app.MapControllers();

app.Run();
