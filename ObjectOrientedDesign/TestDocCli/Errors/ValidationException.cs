namespace TestDocCli.Errors;

public class ValidationException(string message) : KnownUserErrorException(message, 3)
{
}
