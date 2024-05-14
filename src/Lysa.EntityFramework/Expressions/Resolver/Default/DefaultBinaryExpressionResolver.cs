using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Lysa.EntityFramework.Expressions;

/// <summary>
///     二元表达式解析器提供者
/// </summary>
public class DefaultBinaryExpressionResolver : IExpressionResolver, IBinaryExpressionResolver
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultBinaryExpressionResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     解析二元表达式
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    public string ResolveExpression(Expression expression)
    {
        var binaryExpression = expression as BinaryExpression;
        if (binaryExpression == null)
        {
            return string.Empty;
        }

        switch (binaryExpression.NodeType)
        {
            case ExpressionType.GreaterThan:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " > " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.GreaterThanOrEqual:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " >= " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.LessThan:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " < " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.LessThanOrEqual:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " <= " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.Equal:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " = " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.NotEqual:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " != " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.AndAlso:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " AND " + ResolveBinaryRightExpression(binaryExpression.Right);
            case ExpressionType.OrElse:
                return ResolveBinaryLeftExpression(binaryExpression.Left) + " OR " + ResolveBinaryRightExpression(binaryExpression.Right);
            default:
                return string.Empty;
        }
    }

    /// <summary>
    ///     解析二元表达式左边
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    private string ResolveBinaryLeftExpression(Expression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.MemberAccess:
                var memberExpressionResolver= _serviceProvider.GetRequiredService<IMemberAccessExpressionResolver>();
                return memberExpressionResolver?.ResolveExpression(expression);
            /*case ExpressionType.Constant:
                return ResolveConstantExpression(expression);
            case ExpressionType.Parameter:
                return ResolveParameterExpression(expression);*/
            default:
                return string.Empty;
        }
    }

    /// <summary>
    ///     解析二元表达式右边
    /// </summary>
    /// <param name="expression">表达式</param>
    /// <returns>SQL语句</returns>
    private string ResolveBinaryRightExpression(Expression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.MemberAccess:
                var memberExpressionResolver= _serviceProvider.GetRequiredService<IMemberAccessExpressionResolver>();
                return memberExpressionResolver?.ResolveExpression(expression);
            case ExpressionType.Constant:
                var constantExpressionResolver= _serviceProvider.GetRequiredService<IConstantExpressionResolver>();
                return constantExpressionResolver?.ResolveExpression(expression);
            case ExpressionType.Convert:
                return ResolveBinaryRightExpression((expression as UnaryExpression)?.Operand);
            /*case ExpressionType.Parameter:
                return ResolveParameterExpression(expression);*/
            default:
                return string.Empty;
        }
    }
}