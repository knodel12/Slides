using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExamples
{
    class CPUUsage
    {
        public static void RunCPUTests()
        {
            List<int> myLargeList = Enumerable.Range(0, 100000).ToList();
            HashSet<int> myLargeHashSet = myLargeList.ToHashSet();

            var firstTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                myLargeList.Contains(80000);
            }, true);
            Console.WriteLine($"List Contains: {firstTest.Ticks}");

            var secondTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                myLargeHashSet.Contains(80000);
            }, true);
            Console.WriteLine($"HashSet Contains: {secondTest.Ticks}");

            ILookup<int, int> lu = null;
            var thirdTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                lu = myLargeList.ToLookup(x => x / 10);
            });
            Console.WriteLine($"ToLookup Multiple Values to Same Key: {thirdTest.Ticks}");

            Dictionary<int, List<int>> myDict = new Dictionary<int, List<int>>();
            var fourthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                foreach (var item in myLargeList)
                {
                    var key = item / 10;
                    if (!myDict.ContainsKey(key))
                    {
                        myDict.Add(key, new List<int>());
                    }
                    myDict[key].Add(item);
                }
            });
            Console.WriteLine($"Dictionary with value as IEnumerable: {fourthTest.Ticks}");

            string result = null;
            var fifthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                result = string.Join(',', myLargeList.Where(x => (x / 10) == 40));
            });
            Console.WriteLine($"Finding elements in list by key: {fifthTest.Ticks}, result: {result}");

            var sixthTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                result = string.Join(',', lu[40]);
            });
            Console.WriteLine($"Finding elements in Lookup by key: {sixthTest.Ticks}, result: {result}");

            var seventhTest = Utility.RunMethodAndGetProcessorTime(() =>
            {
                result = string.Join(',', myDict[40]);
            });
            Console.WriteLine($"Finding elements in Dictionary by key: {seventhTest.Ticks}, result: {result}");
        }
    }
}
