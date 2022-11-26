using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;

namespace ExceptionHandler.Extensions
{
    public static class ConfigureSerilogExtension
    {
        public static void ConfigureSerilog(this WebApplicationBuilder application,
                                            WriteTo[] loggers = null,
                                            string connectionString = null)
        {
            var loggerConfiguration = new LoggerConfiguration();

            loggerConfiguration = WriteToOptions(loggerConfiguration, loggers, connectionString);

            Logger log = loggerConfiguration.CreateLogger();

            application.Host.UseSerilog(log);
        }

        private static LoggerConfiguration WriteToOptions(LoggerConfiguration loggerConfiguration, WriteTo[] loggers, string connectionString)
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
                            path: $"./wwwroot/LogEvents/Log.txt",
                            rollingInterval: RollingInterval.Day,
                            restrictedToMinimumLevel: LogEventLevel.Error,
                            outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss} [{Level:u3}] : {Message:lj} {NewLine:2}"
                        ),

                    WriteTo.DataBase => loggerConfiguration.WriteTo.MSSqlServer
                        (
                            connectionString: connectionString,
                            restrictedToMinimumLevel: LogEventLevel.Error,
                            sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true },
                            columnOptions: GetColumnOptions()
                        ),

                    _ => loggerConfiguration
                };
            }

            return loggerConfiguration;
        }

        private static ColumnOptions GetColumnOptions()
        {
            var columnOptions = new ColumnOptions() { };
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);
            return columnOptions;
        }
    }

    public enum WriteTo
    {
        Console,
        File,
        Debug,
        DataBase
    }
}
