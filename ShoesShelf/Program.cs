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
    options.Password.RequireDigit = false; // Nevy�adovat ��slo
    options.Password.RequireLowercase = false; // Nevy�adovat mal� p�smeno
    options.Password.RequireUppercase = false; // Nevy�adovat velk� p�smeno
    options.Password.RequireNonAlphanumeric = false; // Nevy�adovat speci�ln� znak
    options.Password.RequiredLength = 4; // Minim�ln� d�lka hesla nastavena na 4 znaky
})
.AddEntityFrameworkStores<ApplicationDbContext>(); // Ukl�d�n� identit do datab�ze

builder.Services.ConfigureApplicationCookie(options =>
{
    // P�esm�rov�n� na p�ihla�ovac� str�nku p�i pokusu o p��stup bez opr�vn�n�
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
    // Inicializace datab�ze
    await DatabaseSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// I p�esto �e middleware Identity se o autentizaci star� automaticky, nicm�n�, doporu�uje se ho ponechat,
// aby byla aplikace p�ipravena na roz���en� o dal�� autentiza�n� metody a spr�vn� zpracov�n� po�adavk�.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
