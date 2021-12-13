using System;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            Calc calc = new Calc();
            Console.WriteLine("Press 1 to insert file path or 2 to insert mathematical task");
            var input = Console.ReadLine();
            try
            {
                if (input == "1")
                {
                    Console.WriteLine("Input");
                    var path = Console.ReadLine();
                    string[] output = calc.Calculate(FileWorker.FileReader(path));
                    FileWorker.FileWriter(output, path);
                }
                else if (input == "2")
                {
                    Console.WriteLine("Input");
                    var task = Console.ReadLine();
                    decimal output = calc.Calculate(task);
                    Console.WriteLine(output);           
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
