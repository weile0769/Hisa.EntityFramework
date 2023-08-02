namespace Snail.EntityFramework.Models;

/// <summary>
///     实体框架数据库表标签
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SqlTable : Attribute
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="tableName">表名称</param>
    public SqlTable(string tableName)
    {
        TableName = tableName;
    }

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="tableName">表名称</param>
    /// <param name="tableDescription">表描述</param>
    public SqlTable(string tableName, string tableDescription)
    {
        TableName = tableName;
        TableDescription = tableDescription;
    }

    /// <summary>
    ///     表名称
    /// </summary>
    public string TableName { get; set; }

    /// <summary>
    ///     表描述
    /// </summary>
    public string TableDescription { get; set; }
}