
using Moq;
using TestProcessorSolution.ConsoleApp;
using TestProcessorSolution.ConsoleApp.Utility;
namespace TestProcessorSolution.Tests.FilterRulesTest;


public class TextFilterProcessorTests
{
	[Fact]
	public async Task ProcessFileAsync_RemovesExpectedWords()
	{
		// Arrange
		var fileContent = "Alice was beginning to get very tired of sitting by her sister on the bank";

		var mockReader = new Mock<ITextReader>();
		mockReader.Setup(r => r.ReadFromFileAsync("file.txt"))
				  .ReturnsAsync(fileContent);

		var processor = new TextFilterProcessor(mockReader.Object);

		// Act
		var result = await processor.ProcessFileAsync("file.txt");

		// Assert
		Assert.Equal("beginning", result);
		mockReader.Verify(r => r.ReadFromFileAsync("file.txt"), Times.Once);
	}

	[Fact]
	public void ApplyAllFilter_RemovesWordsWithMiddleVowel_LetterT_ShortWords()
	{
		var input = "toe tea the"; // removed by various filters
		var processor = new TextFilterProcessor(new DummyReader());

		var result = processor.ApplyAllFilter(input);

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void ApplyAllFilter_EmptyInput_ReturnsEmpty()
	{
		var processor = new TextFilterProcessor(new DummyReader());

		var result = processor.ApplyAllFilter(string.Empty);

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void ApplyAllFilter_InputWithOnlySpaces_ReturnsEmpty()
	{
		var processor = new TextFilterProcessor(new DummyReader());

		var result = processor.ApplyAllFilter("     ");

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void ApplyAllFilter_WordsWithMixedCaseT_AreRemoved()
	{
		var input = "This Test Text truth";
		var processor = new TextFilterProcessor(new DummyReader());

		var result = processor.ApplyAllFilter(input);

		Assert.Equal(string.Empty, result); // all contain 't' or 'T'
	}

	public class DummyReader : ITextReader
	{
		public Task<string> ReadFromFileAsync(string path)
		{
			return Task.FromResult(string.Empty);
		}
	}

}

