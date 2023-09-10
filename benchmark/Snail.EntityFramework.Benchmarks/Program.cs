using BenchmarkDotNet.Running;
using Snail.EntityFramework.Benchmarks.BenchmarkTests;

BenchmarkRunner.Run<AdoProviderBenchmarkTest>();