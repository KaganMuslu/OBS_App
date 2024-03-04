using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OBS_App.Models
{
	public class IdentityUserSeed
	{
		private static readonly string[] userList = { "Admin", "Ogretmen", "Ogrenci", "Misafir" };
		private static readonly string[] emailList = { "Admin@gmail.com", "Ogretmen@gmail.com", "Ogrenci@gmail.com", "Misafir@gmail.com" };
		private static readonly string[] passwordList = { "Admin0.", "Ogretmen0.", "Ogrenci0.", "Misafir0." };

		public static async void IdentityTestUser(IApplicationBuilder app)
		{
			var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();

			if (context.Database.GetAppliedMigrations().Any())
			{
				context.Database.Migrate();
			}

			var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
			var userRole = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

			var user = await userManager.FindByNameAsync("Misafir");

			if (user == null)
			{

				for (int i = 0; i < userList.Length; i++)
				{

					user = new AppUser
					{

						UserName = userList[i],
						Email = emailList[i],

					};

					await userManager.CreateAsync(user, passwordList[i]);
				}
			}

			var adminUser = await userManager.FindByNameAsync("Admin");

			if (adminUser != null)

			{
				// Admin kullanıcısı bulundu, Admin rolünü atama
				await userManager.AddToRoleAsync(adminUser, "Admin");

			}

			var ogretmenUser = await userManager.FindByNameAsync("Ogretmen");

			if (ogretmenUser != null)
			{
				// Admin kullanıcısı bulundu, Admin rolünü atama

				await userManager.AddToRoleAsync(ogretmenUser, "Ogretmen");

			}
			var ogrenciUser = await userManager.FindByNameAsync("Ogrenci");

			if (ogrenciUser != null)
			{
				// Admin kullanıcısı bulundu, Admin rolünü atama

				await userManager.AddToRoleAsync(ogrenciUser, "Ogrenci");

			}

		}
	}
}
