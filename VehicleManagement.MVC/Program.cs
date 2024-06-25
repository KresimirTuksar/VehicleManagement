using VehicleManagement.Service.Data;
using VehicleManagement.Service.Models.Mappings;
using VehicleManagement.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddAutoMapper(typeof(Program), typeof(AutoMapperProfile));

var app = builder.Build();


app.UseAuthorization();

app.MapControllers(); 

app.Run();
