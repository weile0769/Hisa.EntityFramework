using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Snail.EntityFramework.Benchmarks.BenchmarkTests;


BenchmarkRunner.Run<AdoProviderBenchmarkTest>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));
