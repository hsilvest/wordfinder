using System;

namespace WordFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            WordFinder _wf = new WordFinder(new string[] { "chill", "wind", "snow", "cold", "agis" });
            Console.WriteLine(String.Join(",", _wf.Find(new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" })));
            Console.ReadKey();
        }
    }
}
