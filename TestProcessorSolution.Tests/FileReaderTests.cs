namespace TestProcessorSolution.Tests;

using TestProcessorSolution.ConsoleApp.Utility;

// Async file reading test
public class FileReaderTests
{
	[Fact]
	public async Task ReadAsync_ReadsContentCorrectly()
	{
		// Arrange
		string tempFile = Path.GetTempFileName();
		await File.WriteAllTextAsync(tempFile, "Hello async world");

		var reader = new FileReader();

		// Act
		var content = await reader.ReadAsync(tempFile);

		// Assert
		Assert.Equal("Hello async world", content);

		// Cleanup
		File.Delete(tempFile);
	}
}

