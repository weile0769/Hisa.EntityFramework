using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Snail.EntityFramework.Benchmarks.BenchmarkTests;

BenchmarkRunner.Run<SqlQueryNoSqlParameterBenchmarkTest>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));