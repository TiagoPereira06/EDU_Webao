using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using Logger;
using System;

namespace WebaoBench

{
    public class WebaoBenchConfig : ManualConfig
    {
        public WebaoBenchConfig()
        {
            AddJob(Job.MediumRun
                .WithLaunchCount(1)
                .WithToolchain(InProcessEmitToolchain.Instance)
                .WithId("InProcess"));
        }
    }

    [RankColumn]
    //[Config(typeof(LoggerBenchConfig))]
    public class WebaoBench
    {
        //TODO TESTS
        
    }
    
}