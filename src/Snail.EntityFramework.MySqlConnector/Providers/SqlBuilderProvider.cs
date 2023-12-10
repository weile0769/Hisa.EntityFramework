using Snail.EntityFramework.Models;
using Snail.EntityFramework.Providers;

namespace Snail.EntityFramework.MySqlConnector.Providers;

public class SqlBuilderProvider : ISqlBuilderProvider
{

    public string AppendWhereOrAnd(bool isWhere, string sqlWhere)
    {
        return isWhere ? $" WHERE {sqlWhere}" : $" AND {sqlWhere}";
    }

    public string SqlLeftSymbol()
    {
        return "`";
    }

    public string SqlRightSymbol()
    {
        return "`";
    }

    public string GetColumnName(EntityColumn entityColumn)
    {
        return $"{SqlLeftSymbol()}{entityColumn.ColumnName}{SqlRightSymbol()}";
    }
}