namespace TestProcessorSolution.Tests;

using TestProcessorSolution.ConsoleApp.Utility;

// Async file reading test
public class FileReaderTests
{
	private readonly ITextReader _fileReader;

	public FileReaderTests()
	{
		_fileReader = new FileReader();
	}

	[Fact]
	public async Task ReadAsync_ValidFilePath_ReturnsContent()
	{
		// Arrange
		var path = Path.GetTempFileName();
		await File.WriteAllTextAsync(path, "Sample content");

		// Act
		var result = await _fileReader.ReadFromFileAsync(path);

		// Assert
		Assert.Equal("Sample content", result);

		// Cleanup
		File.Delete(path);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("  ")]
	public async Task ReadAsync_NullOrEmptyPath_ThrowsArgumentException(string path)
	{
		// Act & Assert
		var ex = await Assert.ThrowsAsync<ArgumentException>(() => _fileReader.ReadFromFileAsync(path));
		Assert.Equal("File path must not be null or empty. (Parameter 'path')", ex.Message);
	}

	[Fact]
	public async Task ReadAsync_FileDoesNotExist_ThrowsFileNotFoundException()
	{
		// Arrange
		var path = "./nonexistent.txt";

		// Act & Assert
		var ex = await Assert.ThrowsAsync<FileNotFoundException>(() => _fileReader.ReadFromFileAsync(path));
		Assert.Contains("File does not exist.", ex.Message);
	}

	[Fact]
	public async Task ReadAsync_WhenIOExceptionOccurs_ThrowsWrappedIOException()
	{
		// Arrange
		var path = Path.GetTempFileName();

		// Lock the file to force an IOException
		using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);

		var reader = new FileReader();

		// Act & Assert
		var ex = await Assert.ThrowsAsync<IOException>(() => reader.ReadFromFileAsync(path));
		Assert.Contains("Error reading the file at", ex.Message);

		fs.Close();
		File.Delete(path);
	}
}


