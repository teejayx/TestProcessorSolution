namespace TestProcessorSolution.ConsoleApp.Utility;

public interface ITextReader
{
	Task<string> ReadAsync(string path);
}

