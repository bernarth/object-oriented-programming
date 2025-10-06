namespace TestDocCli.Errors;

public class ExportException(string message, Exception inner) : KnownUserErrorException(message, Codes.ExportFailed, inner)
{
}
