namespace TestProcessorSolution.ConsoleApp.FilterRules;

	public class LetterTFilter : ITextFilter
{
	  public bool ShouldFilter(string word) => word.ToLower().Contains('t');
    }

