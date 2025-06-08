namespace TestProcessorSolution.ConsoleApp.FilterRules;

public class LetterTFilter : ITextFilter
{
	public bool ShouldRemoveWord(string word) => word.ToLower().Contains('t');
}

