using Entities;
using Microsoft.EntityFrameworkCore;

namespace BeanJournal_BackEnd.Helpers
{
	/// <summary>
	/// Helper class to initalize database for docker compose up
	/// </summary>
	public class DatabaseManagementService
	{
		/// <summary>
		/// Migration initialization
		/// </summary>
		/// <param name="app"></param>
		public static void MigrationInitalization(IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!.Database.Migrate();
			}
		}
	}
}
