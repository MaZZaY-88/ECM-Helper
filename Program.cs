using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

namespace ECMHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog from appsettings.json
            // This will load the logging configuration from appsettings.json and environment-specific settings.
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Load general settings from appsettings.json
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true) // Load environment-specific settings (e.g., Development, Production)
                .Build();

            // Configure LoggerService as static
            // Initialize the logger with the configuration settings loaded above.
            LoggerService.ConfigureLogger(configuration);

            try
            {
                // Log the application startup message.
                LoggerService.LogInfo("Starting up");

                // Create a new WebApplication builder using the provided arguments.
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
                // Add MVC controllers with views support.
                builder.Services.AddControllersWithViews();
                // Add support for Razor Pages.
                builder.Services.AddRazorPages();

                // Build the WebApplication.
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    // In non-development environments, use the error handler page.
                    app.UseExceptionHandler("/Home/Error");
                    // Use HTTP Strict Transport Security Protocol (HSTS).
                    app.UseHsts();
                }

                // Redirect HTTP requests to HTTPS.
                app.UseHttpsRedirection();
                // Enable serving static files (e.g., CSS, JavaScript, images).
                app.UseStaticFiles();
                // Configure routing for the application.
                app.UseRouting();
                // Enable authorization middleware.
                app.UseAuthorization();

                // Set up the default route for controllers.
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Set up routing for Razor Pages.
                app.MapRazorPages();

                // Run the application.
                app.Run();
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during application startup.
                LoggerService.LogError("Application start-up failed", ex);
            }
            finally
            {
                // Ensure that all log entries are flushed and resources are released.
                Log.CloseAndFlush();
            }
        }
    }
}
