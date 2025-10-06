namespace TestDocCli.Errors;

// TODO: convert the number type into enum
public sealed class ConfigurationException(string message) : KnownUserErrorException(message, 2)
{
}
