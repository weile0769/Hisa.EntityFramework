namespace Lysa.EntityFramework.MySqlConnector.XUnit.Entities;

/// <summary>
///     测试用户实体
/// </summary>
public class User
{
    // <summary>
    //     标识
    // </summary>
    public long Id { get; set; }

    // <summary>
    //     创建时间
    // </summary>
    public DateTime CreateTime { get; set; }

    // <summary>
    //     最后修改时间
    // </summary>
    public DateTime ModifyTime { get; set; }
}