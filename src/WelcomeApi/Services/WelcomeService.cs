using System;
using System.Threading;
using Microsoft.Extensions.Options;
using WelcomeApi.Options;

namespace WelcomeApi.Services;

public class WelcomeService
{
    private string _welcomeMessage;

    public WelcomeService(IOptions<WelcomeOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        var configuredMessage = options.Value.WelcomeMessage;
        _welcomeMessage = string.IsNullOrWhiteSpace(configuredMessage)
            ? WelcomeOptions.DefaultMessage
            : configuredMessage;
    }

    public string GetWelcomeMessage() => Volatile.Read(ref _welcomeMessage);

    public string UpdateWelcomeMessage(string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(message);
        Interlocked.Exchange(ref _welcomeMessage, message);
        return GetWelcomeMessage();
    }
}
