namespace TestDocCli.Errors;

// SOLVED: convert the number type into enum.The change was made on KnowUserErrorExceptions.cs and in the files where the exit codes where used: 
// ExportException.cs
// ValidationException.cs
// AppHostedService.cs
public sealed class ConfigurationException(string message) : KnownUserErrorException(message, Codes.InvalidConfiguration)
{
}
