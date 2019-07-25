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
                ("Memory Usage", MemoryUsage.RunMemoryTests),
                ("Reflections", Reflection.RunReflectionTests),
                ("CPU Usage", CPUUsage.RunCPUTests)
            };

            while (true)
            {
                Console.WriteLine();
                for (int i = 0; i < programsToRun.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {programsToRun[i].Name}");
                }
                Console.WriteLine();
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
