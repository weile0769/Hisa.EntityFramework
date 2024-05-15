namespace Hisa.EntityFramework.Models;

/// <summary>
///     数据实体
/// </summary>
public class Entity
{
    #region 私有变量

    /// <summary>
    ///     表名称
    /// </summary>
    private string _tableName;

    #endregion

    #region 公有函数

    /// <summary>
    ///     初始化实体列
    /// </summary>
    public void InitEntityColumns()
    {
        foreach (var property in Type.GetProperties())
        {
            var column = new EntityColumn();
            var sqlColumn = property
                .GetCustomAttributes(typeof(SqlColumn), true)
                .OfType<SqlColumn>()
                .FirstOrDefault();
            column.PropertyName = property.Name;
            column.PropertyInfo = property;
            if (sqlColumn == null)
            {
                column.ColumnName = property.Name;
            }
            else
            {
                column.ColumnName = string.IsNullOrWhiteSpace(sqlColumn.ColumnName) ? property.Name : sqlColumn.ColumnName;
                column.ColumnDescription = sqlColumn.ColumnDescription;
                column.Ignore = sqlColumn.Ignore;
            }

            Columns ??= new List<EntityColumn>();
            Columns.Add(column);
        }
    }

    #endregion

    #region 公有属性

    /// <summary>
    ///     实体名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     实体类型
    /// </summary>
    public Type Type { get; set; }

    /// <summary>
    ///     表名称
    /// </summary>
    public string TableName
    {
        get => _tableName ?? Name;
        set => _tableName = value;
    }

    /// <summary>
    ///     表描述
    /// </summary>
    public string TableDescription { get; set; }

    /// <summary>
    ///     表列
    /// </summary>
    public List<EntityColumn> Columns { get; set; } = new();

    #endregion
}