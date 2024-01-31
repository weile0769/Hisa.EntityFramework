using System.Linq.Expressions;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     Lambda表达树解析器接口
/// </summary>
public class DefaultLambdaExpressionProvider:ILambdaExpressionProvider
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultLambdaExpressionProvider()
    {

    }

    /// <summary>
    ///     解析Lambda表达式
    /// </summary>
    /// <param name="expression">Lambda表达式</param>
    /// <typeparam name="T">实体类型</typeparam>
    /// <returns>SQL条件语句</returns>
    public string ResolveLambdaExpression<T>(Expression<Func<T, bool>> expression)
    {
        var conditionBuilderVisitor = new LambdaConditionBuilderVisitor();
        conditionBuilderVisitor.Visit(expression);
        return  conditionBuilderVisitor.ResolveExpression();
    }
}