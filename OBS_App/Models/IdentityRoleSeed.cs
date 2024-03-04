using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OBS_App.Models
{
	public class IdentityRoleSeed
	{
		private static readonly string[] Roles = { "Admin", "Ogretmen", "Ogrenci" };

		public static async void IdentityTestRole(IApplicationBuilder app)
		{
			var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<IdentityContext>();


			if (context.Database.GetAppliedMigrations().Any())
			{
				context.Database.Migrate();
			}

			var userRole = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

			var role = await userRole.FindByNameAsync("Admin");

			if (role == null)
			{
				foreach (var rol in Roles)
				{
					role = new AppRole
					{
						Name = rol
					};

					await userRole.CreateAsync(role);
				}

			}

		}

	}
}
