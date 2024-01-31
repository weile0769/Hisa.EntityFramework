using System.Linq.Expressions;
using Snail.EntityFramework.MySqlConnector.XUnit.Entities;
using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.MySqlConnector.XUnit.UnitTests.Expressions;

/// <summary>
///     DefaultLambdaExpressionProvider单元测试
/// </summary>
[Collection("MySqlConnector数据库驱动测试案例组别")]
public class LambdaExpressionProviderUnitTest
{
    /// <summary>
    ///     Lambda表达树解析器接口
    /// </summary>
    private readonly ILambdaExpressionProvider _lambdaExpressionProvider;

    /// <summary>
    ///     构造函数
    /// </summary>
    public LambdaExpressionProviderUnitTest(ILambdaExpressionProvider lambdaExpressionProvider)
    {
        _lambdaExpressionProvider = lambdaExpressionProvider;
    }

    /// <summary>
    ///     解析Lambda表达式单元测试案例
    /// </summary>
    [Fact(DisplayName = "解析Lambda表达式单元测试案例")]
    public void ResolveLambdaExpressionUnitTest()
    {
        Expression<Func<User, bool>> expression = s => s.Id > 1;
        var result = _lambdaExpressionProvider.ResolveLambdaExpression(expression);
        // Assert
        Assert.Equal(" `Id`  > 1", result);
    }
}