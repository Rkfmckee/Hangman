using Hangman.API.Models;

namespace Hangman.Data
{
    public class Seed
	{
		#region Fields

		private static ApplicationDbContext context;

		#endregion

		#region Seeding Methods

		public static void SeedData(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

				if (context == null)
				{
					throw new Exception("Cannot seed database without required services.");
				}

                context.Database.EnsureDeleted();
				context.Database.EnsureCreated();

                AddWords();

				context.SaveChanges();
			}
		}

		public static void AddWords()
		{
			var reader = new StreamReader("Data/Words.txt");

			while (!reader.EndOfStream)
			{
				var word = reader.ReadLine().Trim().ToUpper();
				context.Words.Add(new Words(word));
			}

			reader.Close();
        }

		#endregion
	}
}
