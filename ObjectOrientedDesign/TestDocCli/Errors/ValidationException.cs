namespace TestDocCli.Errors;

public class ValidationException(string message) : KnownUserErrorException(message, (int)ErrorCodes.ValidationError)
{
}
