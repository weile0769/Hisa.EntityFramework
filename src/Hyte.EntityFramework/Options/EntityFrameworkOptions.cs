using Microsoft.Extensions.DependencyInjection;

namespace Hyte.EntityFramework.Options;

/// <summary>
///     数据库配置选项
/// </summary>
public class EntityFrameworkOptions
{
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