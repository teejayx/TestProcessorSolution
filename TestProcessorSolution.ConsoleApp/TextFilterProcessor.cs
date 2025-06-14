﻿

using TestProcessorSolution.ConsoleApp.FilterRules;
using TestProcessorSolution.ConsoleApp.Utility;
namespace TestProcessorSolution.ConsoleApp;
 public class TextFilterProcessor
{
	private readonly IEnumerable<ITextFilter> _filters; 
	private readonly ITextReader _reader;

	public TextFilterProcessor() : this(new FileReader())
	{
	}

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


	public async Task<string> ProcessFileAsync(string path)
	{
		var content = await _reader.ReadFromFileAsync(path);
		return ApplyAllFilter(content);
	}




	public string ApplyAllFilter(string text)
	{
		var words = text.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', '!', '?' },
					  StringSplitOptions.RemoveEmptyEntries);

		var filtered = words
			.Where(word => !_filters.Any(filter => filter.ShouldRemoveWord(word)));

		return string.Join(' ', filtered);
	}
}

