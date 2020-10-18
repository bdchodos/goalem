using System.Threading.Tasks;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Builder;

namespace Goalem.App
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			IHostBuilder builder = Host.CreateDefaultBuilder(args);

			builder
				.ConfigureWebHost(webBuilder =>
				{
					ConfigureWebDefaults(webBuilder);
					webBuilder.UseStartup<Startup>();
				});

			IHost host = builder.Build();

			await host.RunAsync();
		}

		internal static void ConfigureWebDefaults(IWebHostBuilder builder)
		{
			builder.ConfigureAppConfiguration((ctx, cb) =>
			{
				if (ctx.HostingEnvironment.IsDevelopment())
				{
					StaticWebAssetsLoader.UseStaticWebAssets(ctx.HostingEnvironment, ctx.Configuration);
				}
			});

			builder.UseKestrel((builderContext, options) =>
			{
				options.Configure(builderContext.Configuration.GetSection("Kestrel"));
			});

			builder.ConfigureServices((hostingContext, services) =>
			{
				// Fallback
				services.PostConfigure<HostFilteringOptions>(options =>
				{
					if (options.AllowedHosts == null || options.AllowedHosts.Count == 0)
					{
						// "AllowedHosts": "localhost;127.0.0.1;[::1]"
						var hosts = hostingContext.Configuration["AllowedHosts"]?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
						// Fall back to "*" to disable.
						options.AllowedHosts = (hosts?.Length > 0 ? hosts : new[] { "*" });
					}
				});
				// Change notification
				services.AddSingleton<IOptionsChangeTokenSource<HostFilteringOptions>>(
							new ConfigurationChangeTokenSource<HostFilteringOptions>(hostingContext.Configuration));

				services.AddTransient<IStartupFilter, HostFilteringStartupFilter>();

				if (string.Equals("true", hostingContext.Configuration["ForwardedHeaders_Enabled"], StringComparison.OrdinalIgnoreCase))
				{
					services.Configure<ForwardedHeadersOptions>(options =>
					{
						options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
						// Only loopback proxies are allowed by default. Clear that restriction because forwarders are
						// being enabled by explicit configuration.
						options.KnownNetworks.Clear();
						options.KnownProxies.Clear();
					});

					services.AddTransient<IStartupFilter, ForwardedHeadersStartupFilter>();
				}

				services.AddRouting();
			});

			builder.UseIIS();

			builder.UseIISIntegration();
		}
	}

	internal class HostFilteringStartupFilter : IStartupFilter
	{
		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			return app =>
			{
				app.UseHostFiltering();
				next(app);
			};
		}
	}

	internal class ForwardedHeadersStartupFilter : IStartupFilter
	{
		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			return app =>
			{
				app.UseForwardedHeaders();
				next(app);
			};
		}
	}
}
