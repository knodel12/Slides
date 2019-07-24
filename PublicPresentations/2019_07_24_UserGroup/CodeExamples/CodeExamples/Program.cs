using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var programsToRun = new List<(string Name, Action method)>
            {
                ("Binary Serialization", BinarySerialization.RunSerializationTests),
                ("Delegates", MemoryUsage.RunMemoryTests),
                ("Reflections", Reflection.RunReflectionTests)
            };

            for (int i = 0; i < programsToRun.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {programsToRun[i].Name}");
            }

            while (true)
            {
                Console.Write("Run Program: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result > programsToRun.Count)
                    {
                        Console.WriteLine("Exiting...");
                        break;
                    }
                    else
                    {
                        programsToRun[result - 1].method();
                    }
                }
                else
                {
                    Console.WriteLine("Unknown Input");
                }
            }
        }
    }
}
