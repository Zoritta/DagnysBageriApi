using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DagnysBageriApi;
using DagnysBageriApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));

builder.Services.AddDbContext<DataContext>(options =>
{
    // options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
    options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);

});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; 
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "DagnysBageriApi",         
        ValidAudience = "DagnysBageriFrontend",     
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("D@gnysBageri_SecretKey2025")) 
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")  
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

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

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();
