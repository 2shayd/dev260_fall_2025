using System;

namespace Week2DataStructures
{
    public static class Program
    {
        private static int[] arr = new int[10];
        private static List<int> list = new List<int>();
        private static Stack<string> stack = new Stack<string>();
        private static Queue<string> queue = new Queue<string>();
        private static Dictionary<string, int> dictionary = new Dictionary<string, int>();
        private static HashSet<int> hashSet = new HashSet<int>();

        static void Main(string[] args)
        {

            MyArray();
            MyList();
            MyStack();
            MyQueue();
            MyDictionary();
            MyHashSet();
            if (RunBenchmarks())
            {
                Benchmarks.Run();
            }

        }

        public static void MyArray()
        {
            //A.Array
            arr[0] = 1;
            arr[1] = 2;
            arr[2] = 3;
            Console.WriteLine(arr[2]);
            if (Array.IndexOf(arr, 5) == -1)
            {
                Console.WriteLine("Value not found");
            }
            else
            {
                Console.WriteLine("Value found");
            }
        }
        public static void MyList()
        {
            //B.List<T>
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.Add(5);

            list.Insert(2, 99);

            list.Remove(99);

            // Console.WriteLine("List count: " + list.Count);
        }
        public static void MyStack()
        {
            //C.Stack<T>
            stack.Push("https://www.google.com/");
            stack.Push("https://www.youtube.com/");
            stack.Push("https://www.facebook.com/");
            Console.WriteLine("Current Page: " + stack.Peek());

            while (stack.Count > 0)
            {
                Console.WriteLine("History: " + stack.Peek());
                stack.Pop();
            }
        }
        public static void MyQueue()
        {
            //D.Queue<T>
            queue.Enqueue("Printing job 1");
            queue.Enqueue("Printing job 2");
            queue.Enqueue("Printing job 3");
            Console.WriteLine("Next job: " + queue.Peek());
            while (queue.Count > 0)
            {
                string job = queue.Dequeue();
                Console.WriteLine("Dequeing: " + job);
            }
            Console.WriteLine("Queue: " + queue.Count);
        }
        public static void MyDictionary()
        {

            //E.Dictionary<TKey,TValue>
            dictionary.Add("SKU001", 10);
            dictionary.Add("SKU002", 20);
            dictionary.Add("SKU003", 30);
            dictionary["SKU002"] = 5;
            if (dictionary.TryGetValue("SKU004", out int quantity) == false)
            {
                Console.WriteLine("Missing");
            }
        }
        public static void MyHashSet()
        {
            //F.HashSet<T>
            Console.WriteLine("Adding 1: " + hashSet.Add(1));
            hashSet.Add(2);
            Console.WriteLine("Adding 1 again: " + hashSet.Add(1));
            hashSet.Add(6);
            hashSet.UnionWith(new int[] { 3, 4, 5 });
            Console.WriteLine("HashSet count: " + hashSet.Count);
        }
        public static bool RunBenchmarks()
        {
            Console.WriteLine("Run benchmarks? (y/n): ");
            if (Console.ReadLine().ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
