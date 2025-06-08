namespace TestProcessorSolution.Tests.FilterRulesTest;

using TestProcessorSolution.ConsoleApp.FilterRules;

public class LetterTFilterTests
	{
			private readonly LetterFilter _filter = new();

			[Theory]
			[InlineData("tree", true)]
			[InlineData("time", true)]
			[InlineData("cat", true)]
			[InlineData("run", false)]
			public void ShouldRemove_LetterT(string word, bool expected)
			{
				var result = _filter.ShouldFilter(word);
				Assert.Equal(expected, result);
			}
    }

