using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OBS_App.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Veri Taban� Ba�lant�s�
builder.Services.AddDbContext<IdentityContext>(options => options.UseMySql(
            builder.Configuration["ConnectionStrings:sql_con"], new MySqlServerVersion(new Version(8, 0, 36))));


//Bu kod Identityuser ve Role ��in Gerekli olan �emay� projenin i�ine Dahil ediyor //AddEntityFrameworkStores<IdentityDataContext>(); blgilerin nerede deoplanaca��n� belirtir.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>();


//Authorization  configuration files ayarlar�n� yap�land�r�r(Giri�)
builder.Services.ConfigureApplicationCookie(options =>
{
    //Kullan�c� authorize oldu�unda gelecek sayfa
    options.LoginPath = "/Account/Login";
    //yetkisiz giri�lerde g�nderilen sayfa  
    options.AccessDeniedPath = "/Account/Accessdenied";
    //e�er kullan�c� sitede aktif ise cookie s�resi s�f�rlan�r
    options.SlidingExpiration = true;
    //cookie saklama zaman� - //oturum sonland�rma zaman�.
    options.ExpireTimeSpan = TimeSpan.FromDays(1);

});

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

//Projeye Kimlik giri�i Uygulamas�n� Ekler.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

//IdentitySeed Verisini �al��t�r�r
IdentityUserSeed.IdentityTestUser(app);
IdentityRoleSeed.IdentityTestRole(app);

app.Run();
