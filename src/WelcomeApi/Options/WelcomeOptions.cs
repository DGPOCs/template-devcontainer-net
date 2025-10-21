namespace WelcomeApi.Options;

public class WelcomeOptions
{
    public const string SectionName = "Welcome";
    public const string DefaultMessage = "Bienvenido a Dev Containers con .NET!";

    public string WelcomeMessage { get; set; } = DefaultMessage;
}
