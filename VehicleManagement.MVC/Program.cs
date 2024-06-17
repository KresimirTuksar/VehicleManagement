using Microsoft.AspNetCore.Hosting;
using VehicleManagement.MVC;
using VehicleManagement.Service.Data;
using VehicleManagement.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IVehicleService, VehicleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=VehicleMakes}/{action=Index}/{id?}");

app.Run();
