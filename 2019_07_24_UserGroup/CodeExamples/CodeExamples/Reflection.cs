using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExamples
{
    class Reflection
    {
        public static void RunReflectionTests()
        {
            Type t = null; 
            Type i = null;
            var instance = new MyRandomClass();

            var firstTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                t = typeof(MyRandomClass);
                i = typeof(IMyRandomInterface);
            });
            Console.WriteLine($"Time to Compute Two Types: {firstTest.Ticks}");

            var secondTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                t = typeof(MyRandomClass);
                i = typeof(IMyRandomInterface);
            });
            Console.WriteLine($"Time to Compute Two Types Again: {secondTest.Ticks}");

            var thirdTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                t.GetInterfaces().Contains(i);
            });
            Console.WriteLine($"Time to GetInterfacesContains: {thirdTest.Ticks}");

            bool equalResult = false;
            var fourthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                equalResult = t.Equals(i);
            });
            Console.WriteLine($"Time to Equals: {fourthTest.Ticks}, result: {equalResult}");

            bool equalEqualResult = false;
            var fifthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                equalEqualResult = t == i;
            });
            Console.WriteLine($"Time to ==: {fifthTest.Ticks}, result: {equalEqualResult}");

            bool isAssignableFromResult = false;
            var sixthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                isAssignableFromResult = i.IsAssignableFrom(t);
            });
            Console.WriteLine($"Time for IsAssignableFrom: {sixthTest.Ticks}, result: {isAssignableFromResult}");

            bool isInstanceOfTypeResult = false;
            var seventhTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                isInstanceOfTypeResult = t.IsInstanceOfType(instance);
            });
            Console.WriteLine($"Time for IsInstanceOfType: {seventhTest.Ticks}, result: {isInstanceOfTypeResult}");

            bool isInstances = false;
            var eighthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                isInstances = (instance is IMyRandomInterface);
            });
            Console.WriteLine($"Time for is Operator: {seventhTest.Ticks}, result: {isInstances}");
        }

        class MyRandomClass : IMyRandomInterface, IMyRandomInterface2, IMyRandomInterface3, IMyRandomInterface4, IMyRandomInterface5, IMyRandomInterface6, IMyRandomInterface7
        {

        }

        interface IMyRandomInterface
        {

        }

        interface IMyRandomInterface2
        {

        }

        interface IMyRandomInterface3
        {

        }

        interface IMyRandomInterface4
        {

        }

        interface IMyRandomInterface5
        {

        }

        interface IMyRandomInterface6
        {

        }

        interface IMyRandomInterface7
        {

        }
    }
}
