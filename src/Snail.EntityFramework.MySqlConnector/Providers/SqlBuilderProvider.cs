using Snail.EntityFramework.Models;
using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     SQL构造器
/// </summary>
public class SqlBuilderProvider : ISqlBuilderProvider
{
    /// <summary>
    ///     追加WHERE查询条件
    /// </summary>
    /// <param name="isWhere">是否已经存在WHERE关键字</param>
    /// <param name="sqlWhere">查询条件</param>
    /// <returns>追加拼接后的WHERE查询条件语句</returns>
    public string AppendWhereOrAnd(bool isWhere, string sqlWhere)
    {
        return isWhere ? $" where {sqlWhere}" : $" and {sqlWhere}";
    }

    /// <summary>
    ///     左转义标识符
    /// </summary>
    /// <returns>左转义标识符</returns>
    public string SqlLeftSymbol()
    {
        return "`";
    }

    /// <summary>
    ///     右转义标识符
    /// </summary>
    /// <returns>右转义标识符</returns>
    public string SqlRightSymbol()
    {
        return "`";
    }

    /// <summary>
    ///     带转义标识符的列名
    /// </summary>
    /// <param name="entityColumn">实体列</param>
    /// <returns>带转义标识符的列名</returns>
    public string GetColumnName(EntityColumn entityColumn)
    {
        return $"{SqlLeftSymbol()}{entityColumn.ColumnName}{SqlRightSymbol()}";
    }

    /// <summary>
    ///     带转义标识符的表名
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>带转义标识符的表名</returns>
    public string GetTableName(Entity entity)
    {
        return $"{SqlLeftSymbol()}{entity.TableName}{SqlRightSymbol()}";
    }
}