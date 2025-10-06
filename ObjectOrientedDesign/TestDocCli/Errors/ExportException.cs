namespace TestDocCli.Errors;

public class ExportException(string message, Exception inner) : KnownUserErrorException(message, (int)ErrorCodes.ExportError, inner)
{
}
