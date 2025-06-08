using TestProcessorSolution.ConsoleApp.FilterRules;
namespace TestProcessorSolution.Tests.FilterRulesTest;



public class MiddleVowelFilterTests
	{
		private readonly MiddleVowelFilter _filter = new();

		[Theory]
		[InlineData("clean", true)]
		[InlineData("what", true)]
		[InlineData("currently", true)]
		[InlineData("the", false)]
		[InlineData("rather", false)]
		[InlineData("cry", false)]
		public void ShouldRemove_MiddleVowel(string word, bool expected)
		{
			var result = _filter.ShouldRemove(word);
			Assert.Equal(expected, result);
		}
	}



