using System.Text;

namespace Snail.EntityFramework.Providers;

/// <summary>
/// </summary>
public class QueryBuilderProvider
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public QueryBuilderProvider()
    {
        Sql = new StringBuilder();
    }

    /// <summary>
    ///     SQL脚本
    /// </summary>
    public StringBuilder Sql { get; set; }

    /// <summary>
    /// </summary>
    public object SelectCondition { get; set; }

    /// <summary>
    ///     SQL查询条件
    /// </summary>
    public List<string> WhereConditions { get; set; }

    /// <summary>
    ///     SQL分组条件
    /// </summary>
    public string GroupByCondition { get; set; }

    /// <summary>
    ///     SQL分组过滤条件
    /// </summary>
    public string HavingCondition { get; set; }

    /// <summary>
    ///     SQL排序条件
    /// </summary>
    public string OrderByCondition { get; set; }

    /// <summary>
    ///     是否去重
    /// </summary>
    public bool IsDistinct { get; set; }

    /// <summary>
    ///     实体类型
    /// </summary>
    public Type EntityType { get; set; }

    /// <summary>
    /// </summary>
    public int? Skip { get; set; }

    /// <summary>
    /// </summary>
    public int? Take { get; set; }
}