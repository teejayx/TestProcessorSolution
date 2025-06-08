namespace TestProcessorSolution.ConsoleApp.FilterRules;


	public class MiddleVowelFilter
	{
		private static readonly HashSet<char> Vowels = new() { 'a', 'e', 'i', 'o', 'u' };

		public bool ShouldRemove(string word)
		{
			if (string.IsNullOrWhiteSpace(word)) return false;
			var w = word.ToLower();
			int len = w.Length;
			if (len < 3) return false;

			int mid = len / 2;
			return len % 2 == 0
				? Vowels.Contains(w[mid - 1]) || Vowels.Contains(w[mid])
				: Vowels.Contains(w[mid]);
		}

   }

