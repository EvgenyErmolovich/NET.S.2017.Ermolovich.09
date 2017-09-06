using System;
namespace LogicQueue.ConsoleUI
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Queue<string> queue = new Queue<string>();
			queue.Enqueue("qw");
            queue.Enqueue("aad");
            queue.Enqueue("fsdgfhg");
            queue.Enqueue("urtyre");
            
            foreach (var i in queue)
            {
                Console.WriteLine(i);
            }
		}
	}
}
