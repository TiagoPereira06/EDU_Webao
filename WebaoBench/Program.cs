using Benchmark.Running;

namespace WebaoBench
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<LoggerBench>();
        }
    }
}