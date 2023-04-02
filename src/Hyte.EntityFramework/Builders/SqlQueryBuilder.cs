using Hyte.EntityFramework.Models;

namespace Hyte.EntityFramework.Builders
{
    public class SqlQueryBuilder
    {
        /// <summary>
        ///     查询函数SQL构造器
        /// </summary>
        private readonly IQueryBuilder _queryBuilder;

        /// <summary>
        ///     构造函数
        /// </summary>
        public SqlQueryBuilder(IQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public virtual KeyValuePair<string, List<SqlParameter>> _ToSql()
        {

        }
    }
}
