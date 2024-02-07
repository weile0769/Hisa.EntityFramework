using System.Linq.Expressions;

namespace Snail.EntityFramework.Expressions;

/// <summary>
///     MemberAccess表达式解析器提供者
/// </summary>
/// <remarks>
///     MemberAccess表达式通常指的是对类、结构体或对象的成员（如字段、属性、方法）进行访问的操作
/// </remarks>
public interface IMemberAccessExpressionResolver
{
    /// <summary>
    ///     表达式解析
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    string ResolveExpression(Expression expression);
}