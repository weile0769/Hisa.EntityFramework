using System.Linq.Expressions;

namespace Snail.EntityFramework.Expressions;

/// <summary>
///     Lambda表达树解析器接口
/// </summary>
public interface ILambdaExpressionProvider
{
    /// <summary>
    ///     解析Lambda表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>SQL条件语句</returns>
    string ResolveWhereLambdaExpression<T>(Expression<Func<T, bool>> expression);
}