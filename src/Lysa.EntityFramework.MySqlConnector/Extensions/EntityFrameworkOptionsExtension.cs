namespace Lysa.EntityFramework.Options;

public static class EntityFrameworkOptionsExtension
{
    /// <summary>
    ///     配置 MySqlConnector 数据驱动
    /// </summary>
    /// <param name="options">实体框架配置选项</param>
    /// <param name="optionAction">数据驱动配置选项</param>
    /// <returns>实体框架配置选项</returns>
    public static EntityFrameworkOptions UseMySqlConnector(this EntityFrameworkOptions options, Action<MySqlConnectorOptions> optionAction = default)
    {
        options.RegisterExtension(new MySqlConnectorOptionsExtension(optionAction));
        return options;
    }
}