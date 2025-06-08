namespace TestProcessorSolution.ConsoleApp.FilterRules;

	public class LetterFilter : IFilter
{
	  public bool ShouldFilter(string word) => word.ToLower().Contains('t');
    }

