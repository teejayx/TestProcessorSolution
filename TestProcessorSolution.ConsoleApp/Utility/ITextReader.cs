namespace TestProcessorSolution.ConsoleApp.Utility;

public interface ITextReader
{
	Task<string> ReadFromFileAsync(string path);
}

