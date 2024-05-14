using System.Linq.Expressions;
using Lysa.EntityFramework.Providers;

namespace Lysa.EntityFramework.Expressions;

/// <summary>
///     MemberAccess表达式解析器提供者
/// </summary>
/// <remarks>
///     MemberAccess表达式通常指的是对类、结构体或对象的成员（如字段、属性、方法）进行访问的操作
/// </remarks>
public class DefaultMemberAccessExpressionResolver : IMemberAccessExpressionResolver
{
    private readonly ISqlFormatProvider _sqlFormatProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultMemberAccessExpressionResolver(ISqlFormatProvider sqlFormatProvider)
    {
        _sqlFormatProvider = sqlFormatProvider;
    }

    /// <summary>
    ///     表达式解析
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    public string ResolveExpression(Expression expression)
    {
        string fieldName;
        switch (expression)
        {
            case MemberExpression memberExpression:
                fieldName = memberExpression.Member.Name;
                break;
            case MethodCallExpression methodCallExpression:
                fieldName = methodCallExpression.Method.Name;
                break;
            default:
                return string.Empty;
        }

        return _sqlFormatProvider.GetColumnName(fieldName);
    }
}