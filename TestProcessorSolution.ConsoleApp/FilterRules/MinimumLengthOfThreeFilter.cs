namespace TestProcessorSolution.ConsoleApp.FilterRules;

	public class MinimumLengthOfThreeFilter : IFilter
{
	  public bool ShouldFilter(string word) => word.Length < 3;
    }

