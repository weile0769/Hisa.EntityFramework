using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Snail.EntityFramework.Expressions;

/// <summary>
///     Lambda表达树解析器接口
/// </summary>
public class DefaultLambdaExpressionProvider : ILambdaExpressionProvider
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultLambdaExpressionProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     解析Where条件Lambda表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>条件SQL语句</returns>
    public string ResolveWhereLambdaExpression<T>(Expression<Func<T, bool>> expression)
    {
        return ResolveLambdaExpression<T>(expression?.Body);
    }

    /// <summary>
    ///     解析Lambda表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>SQL语句</returns>
    private string ResolveLambdaExpression<T>(Expression expression)
    {
        if (expression == null)
        {
            return string.Empty;
        }

        switch (expression)
        {
            case  BinaryExpression:
                var binaryExpressionResolver= _serviceProvider.GetRequiredService<IBinaryExpressionResolver>();
                return binaryExpressionResolver?.ResolveExpression(expression);
            default:
                return string.Empty;
        }
    }
}