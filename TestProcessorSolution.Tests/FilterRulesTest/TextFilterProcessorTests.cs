namespace TestProcessorSolution.Tests.FilterRulesTest;

using TestProcessorSolution.ConsoleApp;
using TestProcessorSolution.ConsoleApp.FilterRules;
using TestProcessorSolution.ConsoleApp.Utility;

public class TextFilterProcessor
{
	private readonly IEnumerable<ITextFilter> _filters;
	private readonly ITextReader _reader;

	public TextFilterProcessor() : this(new FileReader()) { }

	public TextFilterProcessor(ITextReader reader)
	{
		_reader = reader;
		_filters = new List<ITextFilter>
		{
			new MiddleVowelFilter(),
			new MinimumLengthOfThreeFilter(),
			new LetterTFilter()
		};
	}

	public TextFilterProcessor(IEnumerable<ITextFilter> filters)
	{
		_reader = new FileReader();
		_filters = filters;
	}

	public async Task<string> ProcessFileAsync(string path)
	{
		var content = await _reader.ReadFromFileAsync(path);
		return ApplyAllFilter(content);
	}

	public string ApplyAllFilter(string text)
	{
		var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
		var filtered = words.Where(word => !_filters.Any(filter => filter.ShouldRemoveWord(word)));
		return string.Join(' ', filtered);
	}
}

public class TextFilterProcessorTests
{
	[Fact]
	public void Apply_MiddleVowelFilter_RemovesExpectedWords()
	{
		var input = "clean what rather the";
		var processor = new TextFilterProcessor(new[] { new MiddleVowelFilter() });

		var result = processor.ApplyAllFilter(input);
		var expected = "rather the";

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_LetterTFilter_RemovesWordsWithT()
	{
		var input = "this is not a drill";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.ApplyAllFilter(input);
		var expected = "is a drill";

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_MinimumLengthOfThreeFilter_RemovesShortWords()
	{
		var input = "an a I the long word";
		var processor = new TextFilterProcessor(new[] { new MinimumLengthOfThreeFilter() });

		var result = processor.ApplyAllFilter(input);
		var expected = "the long word";

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_EmptyInput_ReturnsEmpty()
	{
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });
		var result = processor.ApplyAllFilter(string.Empty);

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Apply_InputWithOnlySpaces_ReturnsEmpty()
	{
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });
		var result = processor.ApplyAllFilter("     ");

		Assert.Equal(string.Empty, result);
	}

	[Fact]
	public void Apply_InputWithPunctuation_HandlesProperly()
	{
		var input = "Alice, tired. To? The!";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.ApplyAllFilter(input);
		var expected = "Alice,";

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_MixedCase_WordsWithTStillRemoved()
	{
		var input = "This Test Text truth";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.ApplyAllFilter(input);
		var expected = string.Empty;

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

		var result = processor.ApplyAllFilter(input);
		var expected = "beginning";

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

		var result = processor.ApplyAllFilter(input);
		var expected = string.Empty;

		Assert.Equal(expected, result);
	}

	[Fact]
	public void Apply_FilterThatRemovesAllWords_ReturnsEmpty()
	{
		var input = "tall trees try to thrive";
		var processor = new TextFilterProcessor(new[] { new LetterTFilter() });

		var result = processor.ApplyAllFilter(input);
		var expected = string.Empty;

		Assert.Equal(expected, result);
	}
}

