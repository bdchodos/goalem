using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Goalem.App
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			IHost host = Host
				.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				})
				.Build();

			await host.RunAsync();
		}
	}
}
