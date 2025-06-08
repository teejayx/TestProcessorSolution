using TestProcessorSolution.ConsoleApp.FilterRules;
namespace TestProcessorSolution.Tests.FilterRulesTest;

     public class LetterTFilterTests
	{
			private readonly LetterTFilter _filter = new();

			[Theory]
			[InlineData("tree", true)]
			[InlineData("time", true)]
			[InlineData("cat", true)]
			[InlineData("run", false)]
			public void ShouldRemove_LetterT(string word, bool expected)
			{
				var result = _filter.ShouldRemoveWord(word);
				Assert.Equal(expected, result);
			}
    }

