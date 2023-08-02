using Microsoft.Extensions.DependencyInjection;

namespace Snail.EntityFramework.Options;

/// <summary>
///     数据库配置选项
/// </summary>
public class EntityFrameworkOptions
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public EntityFrameworkOptions()
    {
        Extensions = new List<IEntityFrameworkOptionsExtension>();
    }

    /// <summary>
    ///     数据库配置选项
    /// </summary>
    public IList<DatabaseConfigureOptions> ConfigureOptions { get; set; }
    

    /// <summary>
    ///     实体框架配置选项扩展类
    /// </summary>
    internal IList<IEntityFrameworkOptionsExtension> Extensions { get; }

    /// <summary>
    ///     注册实体框架扩展服务
    /// </summary>
    /// <param name="extension">实体框架配置选项扩展类</param>
    public void RegisterExtension(IEntityFrameworkOptionsExtension extension)
    {
        if (extension == null)
        {
            throw new ArgumentNullException(nameof(extension));
        }

        Extensions.Add(extension);
    }
}