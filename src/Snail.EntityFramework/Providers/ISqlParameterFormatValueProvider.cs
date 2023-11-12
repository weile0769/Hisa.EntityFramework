namespace Snail.EntityFramework.Providers;

public interface ISqlParameterFormatValueProvider
{
    object FormatSqlValue(object parameter);
}