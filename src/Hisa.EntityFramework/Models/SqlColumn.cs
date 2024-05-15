namespace Hisa.EntityFramework.Models;

[AttributeUsage(AttributeTargets.Property)]
public class SqlColumn : Attribute
{
    /// <summary>
    ///     列名称
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    ///     列描述
    /// </summary>
    public string ColumnDescription { get; set; }

    /// <summary>
    ///     是否忽略
    /// </summary>
    public bool Ignore { get; set; } = false;

    /// <summary>
    ///     是否主键
    /// </summary>
    public bool PrimaryKey { get; set; }
}