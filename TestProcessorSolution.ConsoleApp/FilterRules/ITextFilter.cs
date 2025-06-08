namespace TestProcessorSolution.ConsoleApp.FilterRules;


	public interface ITextFilter
    {
	  bool ShouldRemoveWord(string word);
    }

