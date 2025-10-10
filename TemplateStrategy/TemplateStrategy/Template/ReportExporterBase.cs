namespace TemplateStrategy.Template;

public abstract class ReportExporterBase
{
  public void Export(TestCaseResult testCaseResult)
  {
    // Here we can have validations
    // Here we can have other rquired logic
    WriteHeader(testCaseResult);
    WriteBody(testCaseResult);
    WriteFooter(testCaseResult);
    Save(testCaseResult);
  }

  protected abstract void WriteHeader(TestCaseResult testCaseResult);
  protected abstract void WriteBody(TestCaseResult testCaseResult);
  protected abstract void WriteFooter(TestCaseResult testCaseResult);

  protected virtual void Save(TestCaseResult testCaseResult)
  {
    Console.WriteLine("Saved in PosgreSQL");
  }
}
