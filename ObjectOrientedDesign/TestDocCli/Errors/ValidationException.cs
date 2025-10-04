namespace TestDocCli.Errors;

public class ValidationException(string message) : KnownUserErrorException(message, Codes.InvalidData)
{
}
