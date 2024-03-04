using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OBS_App.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//Veri Tabaný Baðlantýsý
builder.Services.AddDbContext<IdentityContext>(options => options.UseMySql(
            builder.Configuration["ConnectionStrings:sql_con"], new MySqlServerVersion(new Version(8, 0, 36))));


//Bu kod Identityuser ve Role Ýçin Gerekli olan Þemayý projenin içine Dahil ediyor //AddEntityFrameworkStores<IdentityDataContext>(); blgilerin nerede deoplanacaðýný belirtir.
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<IdentityContext>();


//Authorization  configuration files ayarlarýný yapýlandýrýr(Giriþ)
builder.Services.ConfigureApplicationCookie(options =>
{
    //Kullanýcý authorize olduðunda gelecek sayfa
    options.LoginPath = "/Account/Login";
    //yetkisiz giriþlerde gönderilen sayfa  
    options.AccessDeniedPath = "/Account/Accessdenied";
    //eðer kullanýcý sitede aktif ise cookie süresi sýfýrlanýr
    options.SlidingExpiration = true;
    //cookie saklama zamaný - //oturum sonlandýrma zamaný.
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

//Projeye Kimlik giriþi Uygulamasýný Ekler.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

//IdentitySeed Verisini çalýþtýrýr
IdentityUserSeed.IdentityTestUser(app);
IdentityRoleSeed.IdentityTestRole(app);

app.Run();
