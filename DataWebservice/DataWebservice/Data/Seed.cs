using DataWebservice.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatawebService.Data
{
	public class Seed
	{

		public static async Task InitialSetup(IServiceProvider serviceProvider, IConfiguration configuration)
		{
			var context = new DataWebserviceContext();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

			//Create Roles
			string[] roleNames = { "Super Admin", "Installer", "Demo", "User" };
			IdentityResult roleResult;

			foreach (var name in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(name);

				if (!roleExist)
				{
					roleResult = await roleManager.CreateAsync(new IdentityRole(name));
				}
			}

			//Create Admin
			var email = "admin@gmail.com";
			var password = "Asd@123";

			var admin = new IdentityUser
			{
				UserName = email,
				Email = email
			};

			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				var createAdmin = await userManager.CreateAsync(admin, password);
				if (createAdmin.Succeeded)
				{

					foreach (var role in roleNames)
					{
						await userManager.AddToRoleAsync(admin, role);
					}

				}
			}

		}

	}
}
