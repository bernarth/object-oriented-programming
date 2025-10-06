namespace TestDocCli.Errors;

public class ExportException(string message, Exception inner) : KnownUserErrorException(message, 4, inner)
{
}
