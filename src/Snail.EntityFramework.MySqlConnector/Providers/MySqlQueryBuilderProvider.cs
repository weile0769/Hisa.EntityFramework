using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.MySqlConnector.Providers;

public class MySqlQueryBuilderProvider : QueryBuilderProvider, IQueryBuilderProvider
{
    /// <summary>
    ///     实体映射提供器
    /// </summary>
    private readonly IEntityMappingProvider _entityMappingProvider;

    private readonly ISqlBuilderProvider _sqlBuilderProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public MySqlQueryBuilderProvider(ISqlBuilderProvider sqlBuilderProvider,
        IEntityMappingProvider entityMappingProvider)
    {
        _sqlBuilderProvider = sqlBuilderProvider;
        _entityMappingProvider = entityMappingProvider;
    }


    /// <summary>
    ///     SQL脚本模版
    /// </summary>
    /// <returns></returns>
    public string GetSqlTemplate()
    {
        return "SELECT {0} FROM {1}{2}{3}{4} ";
    }

    public string GetSelectValue()
    {
        var result = string.Empty;
        if (SelectCondition == null || SelectCondition is string)
        {
            var columns = _entityMappingProvider.GetEntity(EntityType).Columns.Where(s => !s.Ignore);
            result = string.Join(",", columns.Select(c => _sqlBuilderProvider.GetColumnName(c)));
        }

        if (IsDistinct)
        {
            result = " DISTINCT " + result;
        }

        return result;
    }

    public string GetWhereCondition()
    {
        if (WhereConditions == null)
        {
            return string.Empty;
        }

        return string.Join(" ", WhereConditions);
    }

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

    public string GetOrderByCondition()
    {
        if (OrderByCondition == null)
        {
            return string.Empty;
        }

        return OrderByCondition;
    }

    public string GetTableName()
    {
        var entity = _entityMappingProvider.GetEntity(EntityType);
        return _sqlBuilderProvider.GetTableName(entity);
    }

    public string ToSql()
    {
        Sql.AppendFormat(GetSqlTemplate(), GetSelectValue(), GetTableName(), GetWhereCondition(), $"{GetGroupByCondition()}{HavingCondition}", Skip != null || Take != null ? null : GetOrderByCondition());
        return Sql.ToString();
    }
}