using Hisa.EntityFramework.Providers;

namespace Lysa.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     IQueryable查询对象提供器
/// </summary>
public class MySqlQueryBuilderProvider : QueryBuilderProvider, IQueryBuilderProvider
{
    private readonly IEntityMappingProvider _entityMappingProvider;
    private readonly ISqlFormatProvider _sqlFormatProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public MySqlQueryBuilderProvider(ISqlFormatProvider sqlFormatProvider,
        IEntityMappingProvider entityMappingProvider)
    {
        _sqlFormatProvider = sqlFormatProvider;
        _entityMappingProvider = entityMappingProvider;
    }

    /// <summary>
    ///     根据预设SQL模版转换SQL
    /// </summary>
    /// <returns>SQL脚本</returns>
    public string ToSql()
    {
        Sql.AppendFormat(GetSqlTemplate(), GetSelectValue(), GetTableName(), GetWhereCondition(), $"{GetGroupByCondition()}{HavingCondition}", Skip != null || Take != null ? null : GetOrderByCondition());
        return Sql.ToString();
    }


    /// <summary>
    ///     SQL脚本模版
    /// </summary>
    /// <returns></returns>
    public string GetSqlTemplate()
    {
        return "SELECT {0} FROM {1}{2}{3}{4} ";
    }

    /// <summary>
    ///     获取查询字段
    /// </summary>
    /// <returns>查询字段</returns>
    public string GetSelectValue()
    {
        var result = string.Empty;
        if (SelectCondition == null || SelectCondition is string)
        {
            var columns = _entityMappingProvider.GetEntity(EntityType).Columns.Where(s => !s.Ignore);
            result = string.Join(",", columns.Select(c => _sqlFormatProvider.GetColumnName(c.ColumnName)));
        }

        if (IsDistinct)
        {
            result = " DISTINCT " + result;
        }

        return result;
    }

    /// <summary>
    ///     获取查询条件
    /// </summary>
    /// <returns>查询条件</returns>
    public string GetWhereCondition()
    {
        if (WhereConditions == null)
        {
            return string.Empty;
        }

        return string.Join(" ", WhereConditions);
    }

    /// <summary>
    ///     获取分组条件
    /// </summary>
    /// <returns>分组条件</returns>
    public string GetGroupByCondition()
    {
        if (GroupByCondition == null)
        {
            return string.Empty;
        }

        if (GroupByCondition.Last() != ' ')
        {
            return $"{GroupByCondition} ";
        }

        return GroupByCondition;
    }

    /// <summary>
    ///     获取排序条件
    /// </summary>
    /// <returns>排序条件</returns>
    public string GetOrderByCondition()
    {
        if (OrderByCondition == null)
        {
            return string.Empty;
        }

        return OrderByCondition;
    }

    /// <summary>
    ///     获取表名
    /// </summary>
    /// <returns>表名</returns>
    public string GetTableName()
    {
        var entity = _entityMappingProvider.GetEntity(EntityType);
        return _sqlFormatProvider.GetTableName(entity.TableName);
    }
}