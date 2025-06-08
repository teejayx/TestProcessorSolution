namespace TestProcessorSolution.Tests.FilterRulesTest;

using TestProcessorSolution.ConsoleApp;
using TestProcessorSolution.ConsoleApp.FilterRules;

public class TextFilterProcessorTests
{

	[Fact]
	public void Apply_AllFilters_RemovesExpectedWords()
	{
		// Arrange
		var input = "Alice was beginning to get very tired of sitting by her sister on the bank";

		var filters = new ITextFilter[]
		{
			new MiddleVowelFilter(),
			new LetterTFilter(),
			new MinimumLengthOfThreeFilter()
		};

		var pipeline = new TextFilterProcessor(filters);

		// Act
		var result = pipeline.Apply(input);

		// Assert
		var expectedWords = new[] { "Alice", "very", "bank" }; // filtered words: was, to, get, tired, of, sitting, by, her, sister, on, the
		var actualWords = result.Split(' ', StringSplitOptions.RemoveEmptyEntries);

		Assert.Equal(expectedWords, actualWords);
	}



	[Fact]
	public void Apply_NoFilters_ReturnsOriginal()
	{
		var input = "Hello world this is a test";
		var pipeline = new TextFilterProcessor(Array.Empty<ITextFilter>());

		var result = pipeline.Apply(input);
		Assert.Equal(input, result);
	}
}

