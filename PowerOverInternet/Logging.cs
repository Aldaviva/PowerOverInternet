using System.Globalization;
using slf4net;
using slf4net.Factories;
using ILogger = slf4net.ILogger;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace PowerOverInternet;

internal class MicrosoftLoggerFactory: NamedLoggerFactoryBase {

    private readonly ILoggerFactory microsoftLoggerFactory;

    public MicrosoftLoggerFactory(ILoggerFactory microsoftLoggerFactory) {
        this.microsoftLoggerFactory = microsoftLoggerFactory;
    }

    protected override ILogger CreateLogger(string name) {
        return new MicrosoftLoggerAdapter(microsoftLoggerFactory.CreateLogger(name), name);
    }

}

internal class MicrosoftLoggerAdapter: NamedLoggerBase, ILogger {

    private static readonly CultureInfo DEFAULT_PROVIDER = CultureInfo.InvariantCulture;
    private static readonly object?[]   NO_ARGS          = Array.Empty<object?>();

    private readonly Microsoft.Extensions.Logging.ILogger logger;

    public MicrosoftLoggerAdapter(Microsoft.Extensions.Logging.ILogger logger, string name): base(name) {
        this.logger = logger;
    }

    private bool isLevelEnabled(LogLevel level) => logger.IsEnabled(level);

    public bool IsDebugEnabled => isLevelEnabled(LogLevel.Debug);
    public bool IsTraceEnabled => isLevelEnabled(LogLevel.Trace);
    public bool IsInfoEnabled => isLevelEnabled(LogLevel.Information);
    public bool IsWarnEnabled => isLevelEnabled(LogLevel.Warning);
    public bool IsErrorEnabled => isLevelEnabled(LogLevel.Error);

    private void log(LogLevel level, string format, object?[]? args = null, IFormatProvider? provider = null, Exception? exception = null) {
        if (isLevelEnabled(level)) {
#pragma warning disable CA2254 // templates are consumer-supplied
            logger.Log(level, exception, string.Format(provider ?? DEFAULT_PROVIDER, format, args ?? NO_ARGS));
#pragma warning restore CA2254
        }
    }

    public void Debug(string message) {
        log(LogLevel.Debug, message);
    }

    public void Debug(string format, params object[] args) {
        log(LogLevel.Debug, format, args);
    }

    public void Debug(IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Debug, format, args, provider);
    }

    public void Debug(Exception exception, string message) {
        log(LogLevel.Debug, message, null, null, exception);
    }

    public void Debug(Exception exception, string format, params object[] args) {
        log(LogLevel.Debug, format, args, null, exception);
    }

    public void Debug(Exception exception, IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Debug, format, args, provider, exception);
    }

    public void Trace(string message) {
        log(LogLevel.Trace, message);
    }

    public void Trace(string format, params object[] args) {
        log(LogLevel.Trace, format, args);
    }

    public void Trace(IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Trace, format, args, provider);
    }

    public void Trace(Exception exception, string message) {
        log(LogLevel.Trace, message, null, null, exception);
    }

    public void Trace(Exception exception, string format, params object[] args) {
        log(LogLevel.Trace, format, args, null, exception);
    }

    public void Trace(Exception exception, IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Trace, format, args, provider, exception);
    }

    public void Warn(string message) {
        log(LogLevel.Warning, message);
    }

    public void Warn(string format, params object[] args) {
        log(LogLevel.Warning, format, args);
    }

    public void Warn(IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Warning, format, args, provider);
    }

    public void Warn(Exception exception, string message) {
        log(LogLevel.Warning, message, null, null, exception);
    }

    public void Warn(Exception exception, string format, params object[] args) {
        log(LogLevel.Warning, format, args, null, exception);
    }

    public void Warn(Exception exception, IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Warning, format, args, provider, exception);
    }

    public void Info(string message) {
        log(LogLevel.Information, message);
    }

    public void Info(string format, params object[] args) {
        log(LogLevel.Information, format, args);
    }

    public void Info(IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Information, format, args, provider);
    }

    public void Info(Exception exception, string message) {
        log(LogLevel.Information, message, null, null, exception);
    }

    public void Info(Exception exception, string format, params object[] args) {
        log(LogLevel.Information, format, args, null, exception);
    }

    public void Info(Exception exception, IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Information, format, args, provider, exception);
    }

    public void Error(string message) {
        log(LogLevel.Error, message);
    }

    public void Error(string format, params object[] args) {
        log(LogLevel.Error, format, args);
    }

    public void Error(IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Error, format, args, provider);
    }

    public void Error(Exception exception, string message) {
        log(LogLevel.Error, message, null, null, exception);
    }

    public void Error(Exception exception, string format, params object[] args) {
        log(LogLevel.Error, format, args, null, exception);
    }

    public void Error(Exception exception, IFormatProvider provider, string format, params object[] args) {
        log(LogLevel.Error, format, args, provider, exception);
    }

}