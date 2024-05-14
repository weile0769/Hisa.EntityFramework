using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Lysa.EntityFramework.Benchmarks.BenchmarkTests;

BenchmarkRunner.Run<SqlQueryNoSqlParameterBenchmarkTest>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));