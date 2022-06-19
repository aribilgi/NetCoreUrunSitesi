using BL;
using DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer()); // uygulamada sql server kullan
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>)); // Dependency Injection y�ntemiyle projemizde IRepository �rne�i istenirse Repository class�ndan instance al�n�p kullan�ma sunulur.

//Di�er Dependency Injection y�ntemleri :

// AddSingleton : Uygulama aya�a kalkarken �al��an ConfigureServices metodunda bu y�ntem ile tan�mlad���m�z her s�n�ftan sadece bir �rnek olu�turulur. Kim nereden �a��r�rsa �a��rs�n kendisine bu �rnek g�nderilir. Uygulama yeniden ba�layana kadar yenisi �retilmez.
// AddTransient : Uygulama �al��ma zaman�nda belirli ko�ullarda �retilir veya varolan �rne�i kullan�r. 
// AddScoped : Uygulama �al���rken her istek i�in ayr� ayr� nesne �retilir.

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

app.MapControllerRoute(
        name: "admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
      );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
