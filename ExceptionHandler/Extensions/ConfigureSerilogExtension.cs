using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace ExceptionHandler.Extensions
{
    public static class ConfigureSerilogExtension
    {
        public static void ConfigureSerilog(this WebApplicationBuilder application,
                                            WriteTo[] loggers = null)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Information();

            loggerConfiguration = WriteToOptions(loggerConfiguration, loggers);

            Logger log = loggerConfiguration.CreateLogger();

            application.Host.UseSerilog(log);
        }

        private static LoggerConfiguration WriteToOptions(LoggerConfiguration loggerConfiguration, WriteTo[] loggers)
        {
            loggers = loggers ?? new WriteTo[]
            {
                WriteTo.Debug
            };

            foreach (var logger in loggers)
            {
                loggerConfiguration = logger switch
                {
                    WriteTo.Console => loggerConfiguration.WriteTo.Console(),

                    WriteTo.Debug => loggerConfiguration.WriteTo.Debug(),

                    WriteTo.File => loggerConfiguration.WriteTo.File
                        (
                            path: "default.txt",
                            rollingInterval: RollingInterval.Day,
                            restrictedToMinimumLevel: LogEventLevel.Error,
                            outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] : {Message:lj} {NewLine}"
                        ),

                    _ => loggerConfiguration
                };
            }

            return loggerConfiguration;
        }
    }

    public enum WriteTo
    {
        Console,
        File,
        Debug
    }
}
