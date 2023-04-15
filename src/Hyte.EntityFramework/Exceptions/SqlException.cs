namespace Hyte.EntityFramework.Exceptions;

/// <summary>
///     数据库执行异常扩展类
/// </summary>
public class SqlException : Exception
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="message">异常内容</param>
    public SqlException(string message) : base(message) { }

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="message">异常内容</param>
    /// <param name="innerException">异常类</param>
    public SqlException(string message, Exception innerException) : base(message, innerException) { }
}