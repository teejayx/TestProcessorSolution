namespace TestProcessorSolution.ConsoleApp.FilterRules;

public class MinimumLengthOfThreeFilter : ITextFilter
{
	public bool ShouldFilter(string word) => word.Length < 3;
}

