using System.Data;
using System.Data.Common;

namespace Snail.EntityFramework.Models;

/// <summary>
///     参数对象
/// </summary>
public class SqlParameter : DbParameter
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public SqlParameter() { }

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="parameterName">属性名称</param>
    /// <param name="parameterValue">属性值</param>
    public SqlParameter(string parameterName, object parameterValue)
    {
        Value = parameterValue;
        ParameterName = parameterName;
        if (parameterValue != null)
        {
            SettingDataType(parameterValue.GetType());
        }
    }


    /// <summary>
    ///     数据类型
    /// </summary>
    public override DbType DbType { get; set; }

    public override ParameterDirection Direction { get; set; }

    /// <summary>
    ///     是否可空变量
    /// </summary>
    public override bool IsNullable { get; set; }

    public override string ParameterName { get; set; }

    public override string SourceColumn { get; set; }
    public override object Value { get; set; }

    public override bool SourceColumnNullMapping { get; set; }
    public override int Size { get; set; }

    public bool IsArray { get; set; }

    public override void ResetDbType()
    {
        DbType = DbType.String;
    }

    private void SettingDataType(Type type)
    {
        switch (type.Name)
        {
            case "Int32":
                DbType = DbType.Int32;
                break;
            case "DateTime":
                DbType = DbType.DateTime;
                break;
            case "DateTimeOffset":
                DbType = DbType.DateTimeOffset;
                break;
        }
    }
}