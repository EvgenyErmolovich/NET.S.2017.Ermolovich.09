using System;
namespace LogicQueue.ConsoleUI
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Queue<string> iqueue = new Queue<string>();
			iqueue.Enqueue("qw");
            iqueue.Enqueue("aad");
            iqueue.Enqueue("fsdgfhg");
            iqueue.Enqueue("urtyre");
            
            foreach (var i in iqueue)
            {
                Console.WriteLine(i);
            }
		}
	}
}
