using Hisa.EntityFramework.Providers;

namespace Hisa.EntityFramework.XUnit;

/// <summary>
///     QueryableProvider单元测试
/// </summary>
[Collection("MySqlConnector数据库驱动测试案例组别")]
public class QueryableProviderUnitTest
{
    /// <summary>
    ///     IQueryable查询对象提供器
    /// </summary>
    private readonly IQueryableProvider<Other> _queryableProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public QueryableProviderUnitTest(IQueryableProvider<Other> queryableProvider)
    {
        _queryableProvider = queryableProvider;
    }

    /// <summary>
    ///     无参数设置SQL查询条件单元测试案例
    /// </summary>
    [Fact(DisplayName = "无参数设置SQL查询条件单元测试案例")]
    public void AppendSqlWhereConditionsNoSqlParameterUnitTest()
    {
        var queryableProvider = _queryableProvider.Where("id>1");
        Assert.NotNull(queryableProvider.WhereConditions);
        Assert.NotEmpty(queryableProvider.WhereConditions);
        Assert.Empty(queryableProvider.SqlParameters);
    }

    /// <summary>
    ///     对象参数化设置SQL查询条件单元测试案例
    /// </summary>
    [Fact(DisplayName = "对象参数化设置SQL查询条件单元测试案例")]
    public void AppendSqlWhereConditionsIncludeObjectParameterUnitTest()
    {
        var queryableProvider = _queryableProvider.Where("id>@id", new
        {
            id = 1
        });
        Assert.NotNull(queryableProvider.WhereConditions);
        Assert.NotEmpty(queryableProvider.WhereConditions);
        Assert.NotNull(queryableProvider.SqlParameters);
        Assert.NotEmpty(queryableProvider.SqlParameters);
        Assert.True(queryableProvider.SqlParameters.Exists(s => int.Parse(s.Value?.ToString() ?? "0") == 1));
    }


    /// <summary>
    ///     无参数设置Lambda查询条件单元测试案例
    /// </summary>
    [Fact(DisplayName = "无参数设置Lambda查询条件单元测试案例")]
    public void AppendLambdaWhereConditionsNoSqlParameterUnitTest()
    {
        var queryableProvider = _queryableProvider.Where(s => s.id > 1);
        Assert.NotNull(queryableProvider.WhereConditions);
        Assert.NotEmpty(queryableProvider.WhereConditions);
        Assert.Empty(queryableProvider.SqlParameters);
    }

    /// <summary>
    ///     非参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化列表查询单元测试案例")]
    public void ToListNoSqlParameterUnitTest()
    {
        var list = _queryableProvider.Queryable().Where("id>1").ToList();
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     对象参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "对象参数化列表查询单元测试案例")]
    public void ToListIncludeObjectParameterUnitTest()
    {
        var list = _queryableProvider.Queryable().Where("id>@id", new
        {
            id = 1
        }).ToList();
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     非参数化列表异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化列表异步查询单元测试案例")]
    public async Task ToListAsyncNoSqlParameterUnitTest()
    {
        var list = await _queryableProvider.Queryable().Where("id>1").ToListAsync();
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     对象参数化列表异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "对象参数化列表异步查询单元测试案例")]
    public async Task ToListAsyncIncludeObjectParameterUnitTest()
    {
        var list = await _queryableProvider.Queryable().Where("id>@id", new
        {
            id = 1
        }).ToListAsync();
        Assert.NotEmpty(list);
    }
}