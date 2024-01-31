using System.Linq.Expressions;

namespace Snail.EntityFramework.Providers;

/// <summary>
///     条件表达式构建器
/// </summary>
public class LambdaConditionBuilderVisitor : ExpressionVisitor
{
    private readonly Stack<string> _stringStack = new();

    /// <summary>
    ///     解析条件表达式
    /// </summary>
    /// <returns>SQL条件语句</returns>
    public string ResolveExpression()
    {
        var condition = string.Concat(_stringStack.ToArray());
        _stringStack.Clear();
        return condition;
    }

    /// <summary>
    ///     解析二元表达式
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected override Expression VisitBinary(BinaryExpression expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        //_stringStack.Push(")");
        //解析右边
        base.Visit(expression.Right);
        _stringStack.Push(" " + ExpressionTypeToOperator(expression.NodeType) + " ");
        //解析左边
        base.Visit(expression.Left);
        //_stringStack.Push("(");

        return expression;
    }

    /// <summary>
    ///     解析属性
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected override Expression VisitMember(MemberExpression expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        if (expression.Expression is ConstantExpression)
        {
            var objectExpression = Expression.Convert(expression, typeof(object));
            var objectValue = Expression.Lambda<Func<object>>(objectExpression).Compile().Invoke();
            _stringStack.Push($" `{objectValue}` ");
        }
        else
        {
            //_stringStack.Push($" [{expression.Member.Name}] ");
            _stringStack.Push($" `{expression.Member.Name}` ");
        }

        return expression;
    }

    /// <summary>
    ///     常量表达式
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected override Expression VisitConstant(ConstantExpression expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        _stringStack.Push($"{expression.Value}");
        return expression;
    }

    /// <summary>
    ///     方法表达式
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    protected override Expression VisitMethodCall(MethodCallExpression expression)
    {
        if (expression == null)
        {
            throw new ArgumentNullException(nameof(expression));
        }

        string format;
        switch (expression.Method.Name)
        {
            case "StartsWith":
                format = "{0} LIKE '{1}%'";
                break;
            case "Contains":
                format = "{0} LIKE '%{1}%'";
                break;
            case "EndsWith":
                format = "{0} LIKE '%{1}'";
                break;
            default:
                throw new NotSupportedException(expression.NodeType + " is not supported!");
        }

        Visit(expression.Object);
        Visit(expression.Arguments[0]);
        var right = _stringStack.Pop();
        var left = _stringStack.Pop();
        _stringStack.Push(string.Format(format, left, right));
        return expression;
    }

    /// <summary>
    ///     表达式类型转换为运算符
    /// </summary>
    /// <param name="type">表达式类型</param>
    /// <returns>运算符</returns>
    private string ExpressionTypeToOperator(ExpressionType type)
    {
        switch (type)
        {
            case ExpressionType.And:
                return "&";
            case ExpressionType.AndAlso:
                return "AND";
            case ExpressionType.OrElse:
                return "OR";
            case ExpressionType.Or:
                return "|";
            case ExpressionType.Not:
                return "NOT";
            case ExpressionType.NotEqual:
                return "<>";
            case ExpressionType.GreaterThan:
                return ">";
            case ExpressionType.GreaterThanOrEqual:
                return ">=";
            case ExpressionType.LessThan:
                return "<";
            case ExpressionType.LessThanOrEqual:
                return "<=";
            case ExpressionType.Equal:
                return "=";
            default:
                throw new Exception("不支持该方法");
        }
    }
}