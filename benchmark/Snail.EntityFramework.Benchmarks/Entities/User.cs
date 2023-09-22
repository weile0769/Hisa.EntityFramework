using SqlSugar;

namespace Snail.EntityFramework.Benchmarks.Entities;

/// <summary>
///     测试用户实体
/// </summary>
[SugarTable("user")]
public class User
{
    /// <summary>
    ///     标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public long Id { get; set; }

    /// <summary>
    ///     创建时间
    /// </summary>
    [SugarColumn(ColumnName = "create_time")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///     最后修改时间
    /// </summary>
    [SugarColumn(ColumnName = "modify_time")]
    public DateTime ModifyTime { get; set; }
}