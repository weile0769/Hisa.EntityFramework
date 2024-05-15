using Hisa.EntityFramework.Expressions.Models;

namespace Hisa.EntityFramework.Expressions;

/// <summary>
///     表达式解析器公共类
/// </summary>
public class IExpressionResolver
{
    public ExpressionResolverContext Context { get; set; }
}