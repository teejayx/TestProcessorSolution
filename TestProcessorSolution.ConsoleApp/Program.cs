
using TestProcessorSolution.ConsoleApp.FilterRules;
using TestProcessorSolution.ConsoleApp.Utility;

namespace TestProcessorSolution.ConsoleApp
{

	internal class Program
	{
		public static async Task Main()
		{
			ITextReader reader = new FileReader();
			var text = await reader.ReadFromFileAsync("Assets/input.txt");

			var filters = new ITextFilter[]
			{
			new MiddleVowelFilter(),
			new MinimumLengthOfThreeFilter(),
			new LetterTFilter()
			};

			var pipeline = new TextFilterProcessor(filters);
			var result = pipeline.Apply(text);

			Console.WriteLine(result);
		}
	}
}
