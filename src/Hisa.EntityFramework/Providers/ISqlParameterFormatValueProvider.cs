namespace Hisa.EntityFramework.Providers;

public interface ISqlParameterFormatValueProvider
{
    object FormatSqlValue(object parameter);
}