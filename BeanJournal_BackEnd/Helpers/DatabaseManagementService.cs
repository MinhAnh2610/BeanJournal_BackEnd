using Entities;
using Microsoft.EntityFrameworkCore;

namespace BeanJournal_BackEnd.Helpers
{
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
