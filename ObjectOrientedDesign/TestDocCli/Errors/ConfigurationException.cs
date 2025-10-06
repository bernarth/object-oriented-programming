namespace TestDocCli.Errors;

// DONE: convert the number type into enum
public sealed class ConfigurationException(string message) : KnownUserErrorException(message, (int)ErrorCodes.ConfigurationError)
{
}
