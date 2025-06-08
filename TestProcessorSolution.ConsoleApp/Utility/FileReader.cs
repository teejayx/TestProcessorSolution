namespace TestProcessorSolution.ConsoleApp.Utility;

public class FileReader : ITextReader
{
	public async Task<string> ReadFromFileAsync(string path)
	{
		if (string.IsNullOrWhiteSpace(path))
			throw new ArgumentException("File path must not be null or empty.", nameof(path));

		if (!File.Exists(path))
			throw new FileNotFoundException("File does not exist.", path);

		try
		{
			using var reader = new StreamReader(path);
			return await reader.ReadToEndAsync();
		}
		catch (IOException ex)
		{
			throw new IOException($"Error reading the file at {path}.", ex);
		}
	}

}
