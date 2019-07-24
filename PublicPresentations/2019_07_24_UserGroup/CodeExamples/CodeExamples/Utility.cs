using System;
using System.Collections.Generic;
using System.Text;

namespace CodeExamples
{
    static class Utility
    {
        public static long RunMethodAndGetMemoryUsage(Action action)
        {
            long memStart = GC.GetTotalMemory(true);
            action();
            long memEnd = GC.GetTotalMemory(true);
            return memEnd - memStart;
        }

        public static MemoryUsageAndCollections RunMethodAndGetMemoryAndGCUsage(Action action)
        {
            var current = GetCurrent();
            action();
            var after = GetCurrent();
            return after - current;
        }

        public static TimeSpan RunMethodAndGetProcessorTime(Action action)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var currentTime = currentProcess.TotalProcessorTime;
            action();
            var afterTime = currentProcess.TotalProcessorTime;
            return afterTime - currentTime;
        }

        static MemoryUsageAndCollections GetCurrent()
        {
            return new MemoryUsageAndCollections()
            {
                Generation0Collections = GC.CollectionCount(0),
                Generation1Collections = GC.CollectionCount(1),
                Generation2Collections = GC.CollectionCount(2),
                Generation3Collections = GC.CollectionCount(3),
                MemoryUsage = GC.GetTotalMemory(true)
            };
        }

        internal class MemoryUsageAndCollections
        {
            public int Generation0Collections { get; set; }
            public int Generation1Collections { get; set; }
            public int Generation2Collections { get; set; }
            public int Generation3Collections { get; set; }
            public long MemoryUsage { get; set; }

            public static MemoryUsageAndCollections operator -(MemoryUsageAndCollections item1, MemoryUsageAndCollections item2)
            {
                return new MemoryUsageAndCollections()
                {
                    Generation0Collections = item1.Generation0Collections - item2.Generation0Collections,
                    Generation1Collections = item1.Generation1Collections - item2.Generation1Collections,
                    Generation2Collections = item1.Generation2Collections - item2.Generation2Collections,
                    Generation3Collections = item1.Generation3Collections - item2.Generation3Collections,
                    MemoryUsage = item1.MemoryUsage - item2.MemoryUsage
                };
            }

            public override string ToString()
            {
                return $"Memory Usage - {MemoryUsage}, Gen0 - {Generation0Collections}, Gen1 - {Generation1Collections}, Gen2 - {Generation2Collections}, Gen3 - {Generation3Collections}";
            }
        }
    }
}
