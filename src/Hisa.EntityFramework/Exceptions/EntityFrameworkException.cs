namespace Hisa.EntityFramework.Exceptions;

/// <summary>
///     数据库执行异常扩展类
/// </summary>
public class EntityFrameworkException : Exception
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="message">异常内容</param>
    /// <param name="args">额外参数</param>
    public EntityFrameworkException(string message, params object[] args) : base(string.Format(message, args)) { }

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="exception">异常类</param>
    /// <param name="message">异常内容</param>
    /// <param name="args">额外参数</param>
    public EntityFrameworkException(Exception exception, string message, params object[] args) : base(string.Format(message, args), exception) { }
}