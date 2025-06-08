
using TestProcessorSolution.ConsoleApp.FilterRules;
namespace TestProcessorSolution.ConsoleApp;
 public class TextFilterProcessor
{
	private readonly IEnumerable<ITextFilter> _filters;

	public TextFilterProcessor(IEnumerable<ITextFilter> filters)
	{
		_filters = filters;
	}

	public string Apply(string text)
	{
		var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

		var filtered = words
			.Where(word => !_filters.Any(filter => filter.ShouldRemoveWord(word)));

		return string.Join(' ', filtered);
	}
}

