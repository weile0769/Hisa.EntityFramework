using System.Linq.Expressions;

namespace Snail.EntityFramework.Expressions;

/// <summary>
///     常量表达式解析器提供者
/// </summary>
public class DefaultConstantExpressionResolver : IConstantExpressionResolver
{
    /// <summary>
    ///     表达式解析
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    public string ResolveExpression(Expression expression)
    {
        switch (expression)
        {
            case ConstantExpression constantExpression:
                return constantExpression.Value?.ToString() ?? string.Empty;
            default:
                return string.Empty;
        }
    }
}