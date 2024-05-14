using System.Data;
using System.Data.Common;
using Lysa.EntityFramework.Models;

namespace Lysa.EntityFramework.XUnit;

/// <summary>
///     SqlClient单元测试
/// </summary>
[Collection("MySqlConnector数据库驱动测试案例组别")]
public class SqlClientUnitTest
{
    /// <summary>
    ///     数据库访问提供程序
    /// </summary>
    private readonly ISqlClient _sqlClient;

    /// <summary>
    ///     构造函数
    /// </summary>
    public SqlClientUnitTest(ISqlClient sqlClient)
    {
        _sqlClient = sqlClient;
    }

    #region Ado测试案例

    #region 同步

    #region SqlQuery

    /// <summary>
    ///     SQL非参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化列表查询单元测试案例")]
    public void SqlQueryNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>1";
        var list = _sqlClient.Ado.SqlQuery<User>(sql);
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL参数化列表查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化列表查询单元测试案例（数组）")]
    public void SqlQueryIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id";
        var list = _sqlClient.Ado.SqlQuery<User>(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 1
        });
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL参数化列表查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化列表查询单元测试案例（列表）")]
    public void SqlQueryIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id
";
        var list = _sqlClient.Ado.SqlQuery<User>(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 1
            }
        ]);
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL对象参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化列表查询单元测试案例")]
    public void SqlQueryIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id and create_time<@createTime
";
        var list = _sqlClient.Ado.SqlQuery<User>(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.NotEmpty(list);
    }

    #endregion

    #region SqlQuerySingle

    /// <summary>
    ///     SQL非参数化单实体查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化单实体查询单元测试案例")]
    public void SqlQuerySingleNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var entity = _sqlClient.Ado.SqlQuerySingle<User>(sql);
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL参数化单实体查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化单实体查询单元测试案例（列表）")]
    public void SqlQuerySingleIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var entity = _sqlClient.Ado.SqlQuerySingle<User>(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL参数化单实体查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化单实体查询单元测试案例（数组）")]
    public void SqlQuerySingleIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var entity = _sqlClient.Ado.SqlQuerySingle<User>(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL对象参数化单实体查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化单实体查询单元测试案例")]
    public void SqlQuerySingleIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var entity = _sqlClient.Ado.SqlQuerySingle<User>(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.NotNull(entity);
    }

    #endregion

    #region ExecuteCommand

    /// <summary>
    ///     SQL非参数化执行单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化执行单元测试案例")]
    public void ExecuteCommandNoSqlParameterUnitTest()
    {
        var sql = @"
delete from user where id=9999;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = _sqlClient.Ado.ExecuteCommand(sql);
        Assert.True(count > 0);
    }

    /// <summary>
    ///     SQL参数化执行单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化执行单元测试案例（列表）")]
    public void ExecuteCommandIncludeSqlParameterListUnitTest()
    {
        var sql = @"
delete from user where id=@id;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = _sqlClient.Ado.ExecuteCommand(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 9999
            }
        ]);
        Assert.True(count > 0);
    }

    /// <summary>
    ///     SQL参数化执行单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化执行单元测试案例（数组）")]
    public void ExecuteCommandIncludeSqlParameterUnitTest()
    {
        var sql = @"
delete from user where id=@id;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = _sqlClient.Ado.ExecuteCommand(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 9999
        });
        Assert.True(count > 0);
    }

    /// <summary>
    ///     SQL对象参数化执行单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化执行单元测试案例")]
    public void ExecuteCommandIncludeObjectParameterUnitTest()
    {
        var sql = @"
delete from user where id=@id;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = _sqlClient.Ado.ExecuteCommand(sql, new
        {
            id = 9999
        });
        Assert.True(count > 0);
    }

    #endregion

    #region GetDataReader

    /// <summary>
    ///     SQL非参数化数据读取器查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化数据读取器查询单元测试案例")]
    public void GetDataReaderNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        using var dataReader = _sqlClient.Ado.GetDataReader(sql);
        Assert.True(((DbDataReader)dataReader).HasRows);
    }

    /// <summary>
    ///     SQL参数化数据读取器查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据读取器查询单元测试案例（数组）")]
    public void GetDataReaderIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        using var dataReader = _sqlClient.Ado.GetDataReader(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True(((DbDataReader)dataReader).HasRows);
    }

    /// <summary>
    ///     SQL参数化数据读取器查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据读取器查询单元测试案例（列表）")]
    public void GetDataReaderIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        using var dataReader = _sqlClient.Ado.GetDataReader(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True(((DbDataReader)dataReader).HasRows);
    }

    /// <summary>
    ///     SQL对象参数化数据读取器查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化数据读取器查询单元测试案例")]
    public void GetDataReaderIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        using var dataReader = _sqlClient.Ado.GetDataReader(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True(((DbDataReader)dataReader).HasRows);
    }

    #endregion

    #region GetDataSet

    /// <summary>
    ///     SQL非参数化数据结果集查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化数据结果集查询单元测试案例")]
    public void GetDataSetNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var dataSet = _sqlClient.Ado.GetDataSet(sql);
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据结果集查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据结果集查询单元测试案例（列表）")]
    public void GetDataSetIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataSet = _sqlClient.Ado.GetDataSet(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据结果集查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据结果集查询单元测试案例（数组）")]
    public void GetDataSetIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataSet = _sqlClient.Ado.GetDataSet(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    /// <summary>
    ///     SQL对象参数化数据结果集查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化数据结果集查询单元测试案例")]
    public void GetDataSetIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var dataSet = _sqlClient.Ado.GetDataSet(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    #endregion

    #region GetDataTable

    /// <summary>
    ///     SQL非参数化数据表格查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化数据表格查询单元测试案例")]
    public void GetDataTableNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var dataTable = _sqlClient.Ado.GetDataTable(sql);
        Assert.True(dataTable.Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据表格查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据表格查询单元测试案例（列表）")]
    public void GetDataTableIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataTable = _sqlClient.Ado.GetDataTable(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True(dataTable.Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据表格查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据表格查询单元测试案例（数组）")]
    public void GetDataTableIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataTable = _sqlClient.Ado.GetDataTable(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True(dataTable.Rows.Count > 0);
    }

    /// <summary>
    ///     SQL对象参数化数据表格查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化数据表格查询单元测试案例")]
    public void GetDataTableIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var dataTable = _sqlClient.Ado.GetDataTable(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True(dataTable.Rows.Count > 0);
    }

    #endregion

    #region GetScalar

    /// <summary>
    ///     SQL非参数化首行首列查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化首行首列查询单元测试案例")]
    public void GetScalarNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=1
";
        var id = _sqlClient.Ado.GetScalar(sql);
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL参数化首行首列查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化首行首列查询单元测试案例（列表）")]
    public void GetScalarIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=@id
";
        var id = _sqlClient.Ado.GetScalar(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL参数化首行首列查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化首行首列查询单元测试案例（数组）")]
    public void GetScalarIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=@id
";
        var id = _sqlClient.Ado.GetScalar(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL对象参数化首行首列查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化首行首列查询单元测试案例")]
    public void GetScalarIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=@id and create_time<@createTime
";
        var id = _sqlClient.Ado.GetScalar(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL对象参数化首行首列查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化首行首列查询单元测试案例")]
    public void GetScalarIncludeObjectListParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id in (@ids) and create_time<@createTime
";
        var id = _sqlClient.Ado.GetScalar(sql, new
        {
            ids = new[] { 1, 2 },
            createTime = DateTime.Now
        });
        Assert.True((long)id > 0);
    }

    #endregion

    #endregion

    #region 异步

    #region SqlQueryAsync

    /// <summary>
    ///     SQL非参数化列表异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化列表异步查询单元测试案例")]
    public async Task SqlQueryAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>1
";
        var list = await _sqlClient.Ado.SqlQueryAsync<User>(sql);
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL参数化列表异步查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化列表异步查询单元测试案例（数组）")]
    public async Task SqlQueryAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id
";
        var list = await _sqlClient.Ado.SqlQueryAsync<User>(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 1
        });
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL参数化列表异步查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化列表异步查询单元测试案例（列表）")]
    public async Task SqlQueryAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id
";
        var list = await _sqlClient.Ado.SqlQueryAsync<User>(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 1
            }
        ]);
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     SQL对象参数化列表异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化列表异步查询单元测试案例")]
    public async Task SqlQueryAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id>@id and create_time<@createTime
";
        var list = await _sqlClient.Ado.SqlQueryAsync<User>(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.NotEmpty(list);
    }

    #endregion

    #region SqlQuerySingleAsync

    /// <summary>
    ///     SQL非参数化单实体异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化单实体异步查询单元测试案例")]
    public async Task SqlQuerySingleAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var entity = await _sqlClient.Ado.SqlQuerySingleAsync<User>(sql);
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL参数化单实体异步查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化单实体异步查询单元测试案例（列表）")]
    public async Task SqlQuerySingleAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var entity = await _sqlClient.Ado.SqlQuerySingleAsync<User>(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL参数化单实体异步查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化单实体异步查询单元测试案例（数组）")]
    public async Task SqlQuerySingleAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var entity = await _sqlClient.Ado.SqlQuerySingleAsync<User>(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.NotNull(entity);
    }

    /// <summary>
    ///     SQL对象参数化单实体异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化单实体异步查询单元测试案例")]
    public async Task SqlQuerySingleAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var entity = await _sqlClient.Ado.SqlQuerySingleAsync<User>(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.NotNull(entity);
    }

    #endregion

    #region ExecuteCommandAsync

    /// <summary>
    ///     SQL非参数化异步执行单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化异步执行单元测试案例")]
    public async Task ExecuteCommandAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
delete from user where id=9999;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = await _sqlClient.Ado.ExecuteCommandAsync(sql);
        Assert.True(count > 0);
    }

    /// <summary>
    ///     SQL参数化异步执行单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化异步执行单元测试案例（列表）")]
    public async Task ExecuteCommandAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
delete from user where id=@id;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = await _sqlClient.Ado.ExecuteCommandAsync(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 9999
            }
        ]);
        Assert.True(count > 0);
    }

    /// <summary>
    ///     SQL参数化异步执行单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化异步执行单元测试案例（数组）")]
    public async Task ExecuteCommandAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
delete from user where id=@id;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = await _sqlClient.Ado.ExecuteCommandAsync(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 9999
        });
        Assert.True(count > 0);
    }

    /// <summary>
    ///     SQL对象参数化异步执行单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化异步执行单元测试案例")]
    public async Task ExecuteCommandAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
delete from user where id=@id;
insert into user(id,create_time,modify_time)
value (9999,now(),now());
";
        var count = await _sqlClient.Ado.ExecuteCommandAsync(sql, new
        {
            id = 9999
        });
        Assert.True(count > 0);
    }

    #endregion

    #region GetDataReader

    /// <summary>
    ///     SQL非参数化数据读取器异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化数据读取器异步查询单元测试案例")]
    public async Task GetDataReaderAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        await using var dataReader = await _sqlClient.Ado.GetDataReaderAsync(sql);
        Assert.True(dataReader.HasRows);
    }

    /// <summary>
    ///     SQL参数化数据读取器异步查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据读取器异步查询单元测试案例（数组）")]
    public async Task GetDataReaderAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        await using var dataReader = await _sqlClient.Ado.GetDataReaderAsync(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True(dataReader.HasRows);
    }

    /// <summary>
    ///     SQL参数化数据读取器异步查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据读取器异步查询单元测试案例（列表）")]
    public async Task GetDataReaderAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        await using var dataReader = await _sqlClient.Ado.GetDataReaderAsync(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True(dataReader.HasRows);
    }

    /// <summary>
    ///     SQL对象参数化数据读取器异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化数据读取器异步查询单元测试案例")]
    public async Task GetDataReaderAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        await using var dataReader = await _sqlClient.Ado.GetDataReaderAsync(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True(dataReader.HasRows);
    }

    #endregion

    #region GetDataSetAsync

    /// <summary>
    ///     SQL非参数化数据结果集异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化数据结果集异步查询单元测试案例")]
    public async Task GetDataSetAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var dataSet = await _sqlClient.Ado.GetDataSetAsync(sql);
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据结果集异步查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据结果集异步查询单元测试案例（列表）")]
    public async Task GetDataSetAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataSet = await _sqlClient.Ado.GetDataSetAsync(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据结果集异步查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据结果集异步查询单元测试案例（数组）")]
    public async Task GetDataSetAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataSet = await _sqlClient.Ado.GetDataSetAsync(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    /// <summary>
    ///     SQL对象参数化数据结果集异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化数据结果集异步查询单元测试案例")]
    public async Task GetDataSetAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var dataSet = await _sqlClient.Ado.GetDataSetAsync(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True(dataSet.Tables.Count > 0);
        Assert.True(dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0);
    }

    #endregion

    #region GetDataTableAsync

    /// <summary>
    ///     SQL非参数化数据表格异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化数据表格异步查询单元测试案例")]
    public async Task GetDataTableAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=1
";
        var dataTable = await _sqlClient.Ado.GetDataTableAsync(sql);
        Assert.True(dataTable.Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据表格异步查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据表格异步查询单元测试案例（列表）")]
    public async Task GetDataTableAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataTable = await _sqlClient.Ado.GetDataTableAsync(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True(dataTable.Rows.Count > 0);
    }

    /// <summary>
    ///     SQL参数化数据表格异步查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化数据表格异步查询单元测试案例（数组）")]
    public async Task GetDataTableAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id
";
        var dataTable = await _sqlClient.Ado.GetDataTableAsync(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True(dataTable.Rows.Count > 0);
    }

    /// <summary>
    ///     SQL对象参数化数据表格异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化数据表格异步查询单元测试案例")]
    public async Task GetDataTableAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id,
       create_time as CreateTime,
       modify_time as ModifyTime
from user where id=@id and create_time<@createTime
";
        var dataTable = await _sqlClient.Ado.GetDataTableAsync(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True(dataTable.Rows.Count > 0);
    }

    #endregion

    #region GetScalarAsync

    /// <summary>
    ///     SQL非参数化首行首列异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化首行首列异步查询单元测试案例")]
    public async Task GetScalarAsyncNoSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=1
";
        var id = await _sqlClient.Ado.GetScalarAsync(sql);
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL参数化首行首列异步查询单元测试案例（列表）
    /// </summary>
    [Fact(DisplayName = "SQL参数化首行首列异步查询单元测试案例（列表）")]
    public async Task GetScalarAsyncIncludeSqlParameterListUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=@id
";
        var id = await _sqlClient.Ado.GetScalarAsync(sql, [
            new SqlParameter
            {
                DbType = DbType.Int64,
                ParameterName = "id",
                Value = 2
            }
        ]);
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL参数化首行首列异步查询单元测试案例（数组）
    /// </summary>
    [Fact(DisplayName = "SQL参数化首行首列异步查询单元测试案例（数组）")]
    public async Task GetScalarAsyncIncludeSqlParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=@id
";
        var id = await _sqlClient.Ado.GetScalarAsync(sql, new SqlParameter
        {
            DbType = DbType.Int64,
            ParameterName = "id",
            Value = 2
        });
        Assert.True((long)id > 0);
    }

    /// <summary>
    ///     SQL对象参数化首行首列异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL对象参数化首行首列异步查询单元测试案例")]
    public async Task GetScalarAsyncIncludeObjectParameterUnitTest()
    {
        var sql = @"
select id          as Id
from user where id=@id and create_time<@createTime
";
        var id = await _sqlClient.Ado.GetScalarAsync(sql, new
        {
            id = 1,
            createTime = DateTime.Now
        });
        Assert.True((long)id > 0);
    }

    #endregion

    #endregion

    #endregion

    #region Queryable测试案例

    #region ToList测试案例

    /// <summary>
    ///     非参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "SQL非参数化列表查询单元测试案例")]
    public void ToListNoSqlParameterUnitTest()
    {
        var list = _sqlClient.Queryable<Other>().Where("id>1").ToList();
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     对象参数化列表查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "对象参数化列表查询单元测试案例")]
    public void ToListIncludeObjectParameterUnitTest()
    {
        var list = _sqlClient.Queryable<Other>().Where("id>@id", new
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
        var list = await _sqlClient.Queryable<Other>().Queryable().Where("id>1").ToListAsync();
        Assert.NotEmpty(list);
    }

    /// <summary>
    ///     对象参数化列表异步查询单元测试案例
    /// </summary>
    [Fact(DisplayName = "对象参数化列表异步查询单元测试案例")]
    public async Task ToListAsyncIncludeObjectParameterUnitTest()
    {
        var list = await _sqlClient.Queryable<Other>().Where("id>@id", new
        {
            id = 1
        }).ToListAsync();
        Assert.NotEmpty(list);
    }

    #endregion

    #endregion
}