namespace TestProcessorSolution.ConsoleApp.FilterRules;

public class MinimumLengthOfThreeFilter : ITextFilter
{
	public bool ShouldRemoveWord(string word) => word.Length < 3;
}

