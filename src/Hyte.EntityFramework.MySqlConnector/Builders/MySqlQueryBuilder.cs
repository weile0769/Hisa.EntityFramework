using Hyte.EntityFramework.Builders;
using System.Text;

namespace Hyte.EntityFramework.MySqlConnector.Builders
{
    /// <summary>
    ///     查询函数SQL构造器
    /// </summary>
    /// <remarks>
    ///     MySqlConnector 驱动实现
    /// </remarks>
    internal class MySqlQueryBuilder : IQueryBuilder
    {
        /// <summary>
        ///     SQL查询字段
        /// </summary>
        private object _selectValue { get; set; }

        /// <summary>
        ///     构造函数
        /// </summary>
        public MySqlQueryBuilder() { }

        #region 设置SQL查询字段

        /// <summary>
        ///     设置SQL查询字段
        /// </summary>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        public MySqlQueryBuilder SetSelectValue(object selectValue)
        {
            _selectValue = selectValue;
            return this;
        }

        #endregion

        #region SQL查询模板

        /// <summary>
        ///     SQL模板
        /// </summary>
        public string GetSqlTemplate()
        {
            return "SELECT {0} FROM {1}{2}{3}{4} ";
        }

        #endregion

        #region SQL查询字段

        /// <summary>
        ///     获取SQL查询字段
        /// </summary>
        /// <returns>查询字段</returns>
        public string GetSelectValue()
        {
            return "*";
            //string result = string.Empty;
            //if (_selectValue == null || _selectValue is string)
            //{
            //    result = GetSelectStringValue();
            //}
            //return result;
        }

        #endregion

        #region SQL查询表名

        public string GetTableName()
        {
            return "*";
        }

        #endregion

        #region SQL查询条件

        public string GetWhereValue()
        {
            return "*";
        }

        #endregion

        #region SQL分组条件

        public string GetGroupByValue()
        {
            return "*";
        }

        #endregion

        #region SQL排序条件

        public string GetOrderByValue()
        {
            return "*";
        }

        #endregion

        #region SQL查询语句

        /// <summary>
        ///     SQL查询语句
        /// </summary>
        /// <returns></returns>
        public string ToSqlString()
        {
            var sql = new StringBuilder();
            sql.AppendFormat(GetSqlTemplate(), GetSelectValue(), GetTableName(), GetWhereValue(), GetGroupByValue(), GetOrderByValue());
            return sql.ToString();
        }

        #endregion
    }
}
