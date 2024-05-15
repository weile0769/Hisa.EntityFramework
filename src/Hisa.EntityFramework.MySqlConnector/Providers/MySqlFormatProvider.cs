using Hisa.EntityFramework.Providers;

namespace Lysa.EntityFramework.MySqlConnector.Providers;

/// <summary>
///     SQL语句格式化器
/// </summary>
public class MySqlFormatProvider : ISqlFormatProvider
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
    /// <param name="columnName">列名</param>
    /// <returns>带转义标识符的列名</returns>
    public string GetColumnName(string columnName)
    {
        return $"{SqlLeftSymbol()}{columnName}{SqlRightSymbol()}";
    }

    /// <summary>
    ///     带转义标识符的表名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>带转义标识符的表名</returns>
    public string GetTableName(string tableName)
    {
        return $"{SqlLeftSymbol()}{tableName}{SqlRightSymbol()}";
    }
}