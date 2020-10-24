using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Goalem.App
{
	public class Startup
	{
		public Startup(
			IConfiguration configuration,
			IWebHostEnvironment environment)
		{
			Configuration = configuration;
			Environment = environment;
		}

		public IConfiguration Configuration { get; }

		public IWebHostEnvironment Environment { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			IMvcCoreBuilder builder = services
				.AddMvcCore()
				.AddApiExplorer()
				.AddAuthorization()
				.AddCors()
				.AddDataAnnotations()
				.AddFormatterMappings()
				.AddViews()
				.AddRazorViewEngine()
				.AddCacheTagHelper();

			AddTagHelpersFrameworkParts(builder.PartManager);

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app)
		{
			if (Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (Environment.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}

		internal static void AddTagHelpersFrameworkParts(ApplicationPartManager partManager)
		{
			Assembly mvcTagHelpersAssembly = typeof(InputTagHelper).GetTypeInfo().Assembly;
			if (!partManager.ApplicationParts.OfType<AssemblyPart>().Any(p => p.Assembly == mvcTagHelpersAssembly))
			{
				partManager.ApplicationParts.Add(new FrameworkAssemblyPart(mvcTagHelpersAssembly));
			}

			Assembly mvcRazorAssembly = typeof(UrlResolutionTagHelper).GetTypeInfo().Assembly;
			if (!partManager.ApplicationParts.OfType<AssemblyPart>().Any(p => p.Assembly == mvcRazorAssembly))
			{
				partManager.ApplicationParts.Add(new FrameworkAssemblyPart(mvcRazorAssembly));
			}
		}

		private class FrameworkAssemblyPart : AssemblyPart, ICompilationReferencesProvider
		{
			public FrameworkAssemblyPart(Assembly assembly)
				: base(assembly)
			{
			}

			IEnumerable<string> ICompilationReferencesProvider.GetReferencePaths()
				=> Enumerable.Empty<string>();
		}
	}
}
