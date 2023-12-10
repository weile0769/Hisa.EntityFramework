using System.Text;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     
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
    ///     排序
    /// </summary>
    public string OrderByCriteria { get; set; }

    /// <summary>
    ///     SQL脚本
    /// </summary>
    public StringBuilder Sql { get; set; }

    /// <summary>
    ///     
    /// </summary>
    public object SelectValue { get; set; }

    /// <summary>
    ///     是否去重
    /// </summary>
    public bool IsDistinct { get; set; }

    /// <summary>
    ///     实体类型
    /// </summary>
    public Type EntityType { get; set; }
}