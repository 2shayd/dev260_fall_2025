using System.Diagnostics;
using Week2DataStructures;

namespace Week2DataStructures
{
    public class Benchmarks
    {
        public static void Run()
        {
            int n = 1000;
            long listMs;
            long hashSetMs;
            // long dictionaryMs;


            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
            {
                Program.MyList();
            }
            sw.Stop();
            listMs = sw.ElapsedMilliseconds;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < n; i++)
            {
                Program.MyHashSet();
            }
            sw.Stop();
            hashSetMs = sw.ElapsedMilliseconds;
            // sw.Reset();
            // sw.Start();
            // for (int i = 0; i < n; i++)
            // {
            //     Program.MyDictionary();
            // }
            // dictionaryMs = sw.ElapsedMilliseconds;
            


            Console.Clear();
            Console.WriteLine($"N={n}");
            Console.WriteLine($"List.Contains(N-1):\t {listMs} ms");
            Console.WriteLine($"HashSet.Contains:\t {hashSetMs} ms");
            // Console.WriteLine($"Dict.ContainsKey:\t {dictionaryMs} ms");
        }
    }
}
    
