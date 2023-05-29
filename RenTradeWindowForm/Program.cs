using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RenTradeWindowService;
using System;
using System.Threading;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RenTradeWindowForm
{
    static class Program
    {
        private static Mutex mutex = null;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var builder = new HostBuilder()
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 var env = hostingContext.HostingEnvironment;

                 var sharedFolder = Path.Combine(env.ContentRootPath, "..", "Shared");

                 //load the AppSettings first, so that appsettings.json overrwrites it
                 config
                        .AddJsonFile(Path.Combine(sharedFolder, "AppSettings.json"), optional: true)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

                 config.AddEnvironmentVariables();
             })
             .ConfigureServices((hostContext, services) =>
             {
                 IConfiguration configuration = hostContext.Configuration;
                 services.Configure<ServiceConfiguration>(configuration.GetSection(nameof(ServiceConfiguration)));
                 services.AddSingleton<MainForm>();

             });

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                try
                {
                    const string appName = "RenTradeWindowForm";
                    bool createdNew;

                    mutex = new Mutex(true, appName, out createdNew);

                    if (!createdNew)
                    {
                        //app is already running! Exiting the application
                        return;
                    }

                    var mainForm = services.GetRequiredService<MainForm>();
                    Application.Run(mainForm);
                }
                catch (Exception)
                {

                    throw;
                }
            }

            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
        }
    }
}
