namespace Snail.EntityFramework.Utils;

/// <summary>
///     错误消息模板
/// </summary>
public class ErrorMessage
{
    /// <summary>
    ///     连接失败
    /// </summary>
    public const string ConnectionFailed = "连接数据库过程中发生错误，检查数据库连接字符串是否配置正确，错误信息：{0}";

    /// <summary>
    ///     数据库配置失败
    /// </summary>
    public const string DefaultConnectionFailed = "获取数据库配置项过程中发生错误，检查DatabaseConfigureOptions默认配置项是否正确";

    /// <summary>
    ///     实体对象映射失败
    /// </summary>
    public const string EntityMapperBuilderFailed = "实体对象必须拥有无参数的构造函数，实体对象：{0}";

    /// <summary>
    ///     实体对象映射失败
    /// </summary>
    public const string EntityColumnNameNotFound = "实体对象缺失DataRead对象字段定义，字段：{0}";
}