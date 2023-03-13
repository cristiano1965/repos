using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; //ILoggerProvider, Ilogger, LogLevel

namespace Packt.Shared;

public class ConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleLogger();
    }

    public void Dispose()
    {
        
    }
}

public class ConsoleLogger : ILogger
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        // per evitare overlogging filtriamo il log level
        switch(logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Information:
            case LogLevel.None:
                return false;
            case LogLevel.Debug:
            case LogLevel.Warning:
            case LogLevel.Error:
            case LogLevel.Critical:
            default:
                return true;

        };
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        // ci interessa loggare solo le query sql
        if (eventId.Id == 20100)
        { 
            // log the level and event identifier
            Write($"Level: {logLevel}, Event id: {eventId.Id}");

            //only output the state or exception if is exists
            if (state is not null) 
            {
                Write($", State: {state}");
            }

            if (exception is not null) 
            {
                Write($", Exception: {exception.Message}");
            }
            WriteLine();
        }
    }
}
