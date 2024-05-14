using System.Linq.Expressions;

namespace Lysa.EntityFramework.Expressions;

/// <summary>
///     常量表达式解析器提供者
/// </summary>
public interface IConstantExpressionResolver
{
    /// <summary>
    ///     表达式解析
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    string ResolveExpression(Expression expression);
}