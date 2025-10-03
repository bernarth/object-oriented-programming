namespace TestDocCli.Errors;

// SOLVE: convert the number type into enum
public enum ErroCode
{
    Unknown = 0,
    Validation = 1,
    Configuration = 2
}
public sealed class ConfigurationException(string message) : KnownUserErrorException(message, (int)ErroCode.Configuration)
{
}
