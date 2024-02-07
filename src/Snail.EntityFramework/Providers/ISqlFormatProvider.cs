namespace Snail.EntityFramework.Providers;

/// <summary>
///     SQL语句格式化器
/// </summary>
public interface ISqlFormatProvider
{
    /// <summary>
    ///     追加WHERE查询条件
    /// </summary>
    /// <param name="isWhere">是否已经存在WHERE关键字</param>
    /// <param name="sqlWhere">查询条件</param>
    /// <returns>追加拼接后的WHERE查询条件语句</returns>
    string AppendWhereOrAnd(bool isWhere, string sqlWhere);

    /// <summary>
    ///     左转义标识符
    /// </summary>
    /// <returns>左转义标识符</returns>
    string SqlLeftSymbol();

    /// <summary>
    ///     右转义标识符
    /// </summary>
    /// <returns>右转义标识符</returns>
    string SqlRightSymbol();

    /// <summary>
    ///     带转义标识符的列名
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <returns>带转义标识符的列名</returns>
    string GetColumnName(string columnName);

    /// <summary>
    ///     带转义标识符的表名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>带转义标识符的表名</returns>
    string GetTableName(string tableName);
}