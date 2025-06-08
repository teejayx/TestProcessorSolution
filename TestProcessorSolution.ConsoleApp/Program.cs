
using TestProcessorSolution.ConsoleApp.FilterRules;
using TestProcessorSolution.ConsoleApp.Utility;

namespace TestProcessorSolution.ConsoleApp
{

	internal class Program
	{
		public static async Task Main()
		{
			var processor = new TextFilterProcessor();
			var result = await processor.ProcessFileAsync("./Assets/input.txt");
			Console.WriteLine(result);
		}
	}
}
