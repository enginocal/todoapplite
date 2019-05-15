using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace TodoAppLite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .MinimumLevel.Debug()
                            .WriteTo.ColoredConsole(
                            LogEventLevel.Verbose,
                            "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                            .CreateLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .UseSerilog()
                 .UseUrls("http://*:5000")
                 .UseStartup<Startup>();
    }
}
