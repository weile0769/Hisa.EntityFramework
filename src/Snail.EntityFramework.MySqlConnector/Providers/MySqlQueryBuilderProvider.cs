using System.Text;
using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.MySqlConnector.Providers;

public class MySqlQueryBuilderProvider : QueryBuilderProvider, IQueryBuilderProvider
{
    private readonly ISqlBuilderProvider _sqlBuilderProvider;

    /// <summary>
    ///     实体映射提供器
    /// </summary>
    private readonly IEntityMappingProvider _entityMappingProvider;

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
        string result = string.Empty;
        if (SelectValue == null || SelectValue is string)
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

    public string ToSql()
    {
        var orderByCriteria = OrderByCriteria;
        Sql.AppendFormat(GetSqlTemplate(), GetSelectValue())
    }
}