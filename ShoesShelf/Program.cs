using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoesShelf.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Konfigurace politiky hesel
    options.Password.RequireDigit = false; // Nevyžadovat èíslo
    options.Password.RequireLowercase = false; // Nevyžadovat malé písmeno
    options.Password.RequireUppercase = false; // Nevyžadovat velké písmeno
    options.Password.RequireNonAlphanumeric = false; // Nevyžadovat speciální znak
    options.Password.RequiredLength = 4; // Minimální délka hesla nastavena na 4 znaky
})
.AddEntityFrameworkStores<ApplicationDbContext>(); // Ukládání identit do databáze

builder.Services.ConfigureApplicationCookie(options =>
{
    // Pøesmìrování na pøihlašovací stránku pøi pokusu o pøístup bez oprávnìní
    options.AccessDeniedPath = "/Account/Login";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    // Inicializace databáze
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// I pøesto že middleware Identity se o autentizaci stará automaticky, nicménì, doporuèuje se ho ponechat,
// aby byla aplikace pøipravena na rozšíøení o další autentizaèní metody a správné zpracování požadavkù.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
