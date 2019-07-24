using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExamples
{
    class Reflection
    {
        public static void RunReflectionTests()
        {
            Type t = null; 
            Type i = null;
            var firstTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                t = typeof(MyRandomClass);
                i = typeof(IMyRandomInterface);
            });
            Console.WriteLine($"Time to Compute Two Types: {firstTest}");
        }

        class MyRandomClass : IMyRandomInterface
        {

        }

        interface IMyRandomInterface
        {

        }
    }
}
