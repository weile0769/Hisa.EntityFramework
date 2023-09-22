<div align="center">
	<h1 align="center" style="color:#4da7fd"><b>Snail.EntityFramework</b></h1>
</div>
<div align="center">
<span align="center" style="font-weight:bold" >⚡一款NET开源多库的、开箱即用的、功能全面的ORM框架</span>
</div>
<br>
<p align="center">
<img alt="csharp" src="https://img.shields.io/badge/language-csharp-brightgreen.svg">
<img alt="license" src="https://img.shields.io/badge/license-MIT-blue.svg">
</p>

### 🚩 项目介绍
Snail.EntityFramework 一款NET开源多库的、开箱即用的、功能全面的ORM框架。目前数据库仅支持MYSQL。未来一直会致力追求卓越的性能、支持更多的数据库，为开源中国添砖加瓦。

### 🏅 开源地址
[![Gitee](https://shields.io/badge/Gitee-https://gitee.com/weile0796/Snail.EntityFramework-green?logo=gitee&style=flat&logoColor=red)](https://gitee.com/weile0796/Snail.EntityFramework.git)


### 🧪 基准测试
```

BenchmarkDotNet v0.13.8, Windows 10 (10.0.19045.3448/22H2/2022Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2


```
| Method                                                     | pageSize | Mean       | Error     | StdDev    | Median     | Rank | Gen0     | Gen1   | Allocated |
|----------------------------------------------------------- |--------- |-----------:|----------:|----------:|-----------:|-----:|---------:|-------:|----------:|
| **SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest** | **1**        |   **253.2 μs** |   **4.55 μs** |   **7.22 μs** |   **250.9 μs** |    **3** |   **1.4648** |      **-** |   **4.69 KB** |
| SqlQueryNoSqlParameterForSqlSugarBenchmarkTest             | 1        |   276.6 μs |   5.19 μs |   5.55 μs |   275.5 μs |    4 |   2.4414 |      - |   7.72 KB |
| SqlQueryNoSqlParameterForFreeSqlBenchmarkTest              | 1        |   170.4 μs |   3.41 μs |   5.50 μs |   168.3 μs |    1 |   1.4648 |      - |   4.97 KB |
| **SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest** | **10**       |   **281.8 μs** |   **5.55 μs** |   **5.19 μs** |   **281.4 μs** |    **4** |   **1.4648** |      **-** |   **5.91 KB** |
| SqlQueryNoSqlParameterForSqlSugarBenchmarkTest             | 10       |   288.6 μs |   5.59 μs |   5.74 μs |   290.0 μs |    4 |   2.4414 |      - |   8.95 KB |
| SqlQueryNoSqlParameterForFreeSqlBenchmarkTest              | 10       |   180.3 μs |   3.58 μs |   9.80 μs |   179.5 μs |    2 |   2.6855 |      - |   8.23 KB |
| **SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest** | **100**      |   **364.0 μs** |   **9.16 μs** |  **26.14 μs** |   **354.3 μs** |    **6** |   **5.3711** |      **-** |  **17.58 KB** |
| SqlQueryNoSqlParameterForSqlSugarBenchmarkTest             | 100      |   403.5 μs |  12.10 μs |  35.31 μs |   400.6 μs |    7 |   6.3477 |      - |  20.64 KB |
| SqlQueryNoSqlParameterForFreeSqlBenchmarkTest              | 100      |   335.1 μs |   8.64 μs |  25.48 μs |   333.7 μs |    5 |  12.6953 |      - |  40.29 KB |
| **SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest** | **200**      |   **405.6 μs** |   **7.25 μs** |   **6.42 μs** |   **406.7 μs** |    **7** |   **9.7656** |      **-** |  **30.54 KB** |
| SqlQueryNoSqlParameterForSqlSugarBenchmarkTest             | 200      |   566.4 μs |  11.70 μs |  33.58 μs |   559.8 μs |    9 |  10.7422 |      - |   33.6 KB |
| SqlQueryNoSqlParameterForFreeSqlBenchmarkTest              | 200      |   459.4 μs |  10.04 μs |  28.63 μs |   457.5 μs |    8 |  24.4141 |      - |  75.91 KB |
| **SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest** | **500**      |   **665.4 μs** |  **16.00 μs** |  **47.18 μs** |   **657.3 μs** |   **10** |  **21.4844** |      **-** |  **67.38 KB** |
| SqlQueryNoSqlParameterForSqlSugarBenchmarkTest             | 500      |   950.9 μs |  18.85 μs |  33.01 μs |   939.6 μs |   12 |  21.4844 |      - |  70.45 KB |
| SqlQueryNoSqlParameterForFreeSqlBenchmarkTest              | 500      |   886.5 μs |  17.45 μs |  33.61 μs |   885.5 μs |   11 |  58.5938 |      - | 180.71 KB |
| **SqlQueryNoSqlParameterForSnailEntityFrameworkBenchmarkTest** | **1000**     | **1,195.8 μs** | **114.94 μs** | **327.94 μs** | **1,059.6 μs** |   **13** |  **41.0156** |      **-** | **130.09 KB** |
| SqlQueryNoSqlParameterForSqlSugarBenchmarkTest             | 1000     | 1,606.2 μs |  30.70 μs |  69.93 μs | 1,588.0 μs |   15 |  42.9688 |      - | 133.17 KB |
| SqlQueryNoSqlParameterForFreeSqlBenchmarkTest              | 1000     | 1,434.5 μs |  27.72 μs |  34.04 μs | 1,436.0 μs |   14 | 115.2344 | 1.9531 | 356.71 KB |

### 🎉 功能介绍