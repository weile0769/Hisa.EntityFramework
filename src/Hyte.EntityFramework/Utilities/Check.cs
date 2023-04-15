using Hyte.EntityFramework.Exceptions;

namespace Hyte.EntityFramework.Utilities;

/// <summary>
///     异常处理帮助类
/// </summary>
public static class Check
{
    /// <summary>
    ///     抛出异常
    /// </summary>
    /// <param name="message">异常内容</param>
    /// <param name="args">额外参数</param>
    public static void Exception(string message, params object[] args)
    {
        throw new SqlException(string.Format(message, args));
    }
}