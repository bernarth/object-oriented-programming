using System;
using System.IO;

var arguments = Arguments.Parse(args);

if (!File.Exists(arguments.FilePath))
{
  Console.WriteLine("FILE_NOT_FOUND " + arguments.FilePath);
  return;
}

var testResults = TestResultParser.Parse(arguments.FilePath);
var summary = TestProcessor.Process(testResults);
SummaryReporter.Report(summary, arguments);
