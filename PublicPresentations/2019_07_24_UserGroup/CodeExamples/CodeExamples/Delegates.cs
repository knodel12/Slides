using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExamples
{
    class MemoryUsage
    {
        public static void RunMemoryTests()
        {
            // Delegate memory usage
            List<Test> testItems = new List<Test>();
            var firstTest = Utility.RunMethodAndGetMemoryUsage(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    testItems.Add(new Test(SomeMethod));
                }
            });
            Console.WriteLine($"Memory consumption with methods: {firstTest}");

            testItems.Clear();
            var secondTest = Utility.RunMethodAndGetMemoryUsage(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    testItems.Add(new Test(() => { }));
                }
            });
            Console.WriteLine($"Memory consumption with Lambdas: {secondTest}");

            testItems.Clear();
            var thirdTest = Utility.RunMethodAndGetMemoryUsage(() =>
            {
                void SomeLocalFunction()
                {

                }

                for (int i = 0; i < 10000; i++)
                {
                    testItems.Add(new Test(SomeLocalFunction));
                }
            });
            Console.WriteLine($"Memory consumption with Local Functions: {thirdTest}");


            // Closures
            Console.WriteLine();
            Func<int> myDeclaration = null;
            var fourthTest = Utility.RunMethodAndGetMemoryUsage(() =>
            {
                myDeclaration = () => 40;
            });
            myDeclaration();
            Console.WriteLine($"Closure Memory Usage Without Local Variables: {fourthTest}");

            myDeclaration = null;
            var fifthTest = Utility.RunMethodAndGetMemoryUsage(() =>
            {
                byte[] testBytes = new byte[1024 * 1024 * 10];
                myDeclaration = () => testBytes[62];
            });
            myDeclaration();
            Console.WriteLine($"Closure Memory Usage With Local Variables: {fifthTest}");

            // strings
            string myRandomString = "";
            var sixthTest = Utility.RunMethodAndGetMemoryAndGCUsage(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    myRandomString += i.ToString();
                }
            });
            myRandomString.ToString();
            Console.WriteLine("Manual String Concatenation:");
            Console.WriteLine(sixthTest);

            StringBuilder myStringBuilder = new StringBuilder();
            var seventhTest = Utility.RunMethodAndGetMemoryAndGCUsage(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    myStringBuilder.Append(i.ToString());
                }
            });
            myStringBuilder.ToString();
            Console.WriteLine("StringBuilder String Concatenation:");
            Console.WriteLine(seventhTest);
        }

        public static void SomeMethod()
        {

        }

        private class Test
        {
            private Action _myAction;
            public Test(Action a)
            {
                _myAction = a;
            }
        }
    }
}
