using Snail.EntityFramework.Models;

namespace Snail.EntityFramework.Providers;

public interface ISqlBuilderProvider
{
    string AppendWhereOrAnd(bool isWhere, string sqlWhere);

    string SqlLeftSymbol();

    string SqlRightSymbol();

    string GetColumnName(EntityColumn entityColumn);

    string GetTableName(Entity entity);
}