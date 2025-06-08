namespace TestProcessorSolution.Tests.FilterRulesTest;

using TestProcessorSolution.ConsoleApp;
using TestProcessorSolution.ConsoleApp.FilterRules;

public class TextFilterProcessorTests
{
	[Fact]
	public void Apply_MiddleVowelFilter_RemovesExpectedWords()
	{
		var input = "clean what rather the";
		var processor = new TextFilterProcessor(new[] { new MiddleVowelFilter() });

		var result = processor.Apply(input);
		var expected = "rather the"; // "clean" and "what" have vowels in the middle

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_LetterTFilter_RemovesWordsWithT()
	{
		var input = "this is not a drill";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.Apply(input);
		var expected = "is a drill"; // removes "this", "not", 

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_MinimumLengthOfThreeFilter_RemovesShortWords()
	{
		var input = "an a I the long word";
		var processor = new TextFilterProcessor(new[] { new MinimumLengthOfThreeFilter() });

		var result = processor.Apply(input);
		var expected = "the long word"; // removes "an", "a", "I"

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_EmptyInput_ReturnsEmpty()
	{
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });
		var result = processor.Apply(string.Empty);

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Apply_InputWithOnlySpaces_ReturnsEmpty()
	{
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });
		var result = processor.Apply("     ");

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Apply_InputWithPunctuation_HandlesProperly()
	{
		var input = "Alice, tired. To? The!";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.Apply(input);
		// With basic tokenization, punctuation remains attached
		// Words like "tired." or "To?" will still be filtered since they contain 't'
		var expected = "Alice,";

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_MixedCase_WordsWithTStillRemoved()
	{
		var input = "This Test Text truth";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.Apply(input);
		var expected = string.Empty; // all contain 't' or 'T'

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_AllFilters_RealisticExample()
	{
		var input = "Alice was beginning to get very tired of sitting by her sister on the bank";
		var filters = new ITextFilter[]
		{
			new MiddleVowelFilter(),
			new LetterTFilter(),
			new MinimumLengthOfThreeFilter()
		};
		var processor = new TextFilterProcessor(filters);

		var result = processor.Apply(input);
		var expected = "beginning"; // only this survives

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_MultipleFilters_SomeWordsFilteredByAll()
	{
		var input = "toe tea the";
		var processor = new TextFilterProcessor(new ITextFilter[]
		{
			new LetterTFilter(),
			new MiddleVowelFilter(),
			new MinimumLengthOfThreeFilter()
		});

		var result = processor.Apply(input);
		var expected = ""; 

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_FilterThatRemovesAllWords_ReturnsEmpty()
	{
		var input = "tall trees try to thrive";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.Apply(input);
		var expected = string.Empty;

		Assert.Equal(expected, result);
	}
}

