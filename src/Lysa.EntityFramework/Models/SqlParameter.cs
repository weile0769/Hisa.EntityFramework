using System.Data;
using System.Data.Common;

namespace Lysa.EntityFramework.Models;

/// <summary>
///     参数对象
/// </summary>
public class SqlParameter : DbParameter
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public SqlParameter() { }


    /*public SqlParameter(string parameterName, object parameterValue)
    {
        Value = parameterValue;
        ParameterName = parameterName;
        if (parameterValue != null)
        {
            SettingDataType(parameterValue.GetType());
        }
    }*/


    /// <summary>
    ///     数据类型
    /// </summary>
    public override DbType DbType { get; set; }

    public override ParameterDirection Direction { get; set; }

    /// <summary>
    ///     是否可空变量
    /// </summary>
    public override bool IsNullable { get; set; }

    /// <summary>
    ///     属性名称
    /// </summary>
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
}