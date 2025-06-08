using TestProcessorSolution.ConsoleApp.FilterRules;
namespace TestProcessorSolution.Tests.FilterRulesTest;

public class MinimumLengthOfThreeFilterTest
{
	private readonly MinimumLengthOfThreeFilter _filter = new();

	[Theory]
	[InlineData("hi", true)]
	[InlineData("go", true)]
	[InlineData("cat", false)]
	[InlineData("tree", false)]
	public void ShouldRemove_ShortWords(string word, bool expected)
	{
		var result = _filter.ShouldFilter(word);
		Assert.Equal(expected, result);
	}
 }

