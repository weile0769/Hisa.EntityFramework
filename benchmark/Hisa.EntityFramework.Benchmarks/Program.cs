using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Hisa.EntityFramework.Benchmarks.BenchmarkTests;

BenchmarkRunner.Run<SqlQueryNoSqlParameterBenchmarkTest>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));