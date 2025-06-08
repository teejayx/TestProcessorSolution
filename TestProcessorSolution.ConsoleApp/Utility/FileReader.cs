namespace TestProcessorSolution.ConsoleApp.Utility;

public class FileReader : ITextReader
{
	public async Task<string> ReadAsync(string path)
	{
		using var reader = new StreamReader(path);
		return await reader.ReadToEndAsync();
	}
}
