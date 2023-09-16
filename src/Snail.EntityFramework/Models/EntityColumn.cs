using System.Reflection;

namespace Snail.EntityFramework.Models;

/// <summary>
///     实体列
/// </summary>
public class EntityColumn
{
    /// <summary>
    ///     属性
    /// </summary>
    public PropertyInfo PropertyInfo { get; set; }

    /// <summary>
    ///     属性名称
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    ///     是否忽略
    /// </summary>
    public bool Ignore { get; set; } = false;

    /// <summary>
    ///     列名称
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    ///     列描述
    /// </summary>
    public string ColumnDescription { get; set; }
}