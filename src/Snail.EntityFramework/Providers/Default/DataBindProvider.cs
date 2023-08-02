using System.Data;

namespace Snail.EntityFramework.Providers;

public class DataBindProvider : IDataBindProvider
{
    public List<T> ToEntities<T>(IDataReader dataReader)
    {
        var type = typeof(T);
        var nameTypes=GetDataReaderNameTypes(dataReader);
        var fieldNames = nameTypes.Select(s => s.Key).ToList();
    }

    
    
    private List<KeyValuePair<string, string>> GetDataReaderNameTypes(IDataReader dataReader)
    {
        var nameTypes = new List<KeyValuePair<string, string>>();
        for (var i = 0; i < dataReader.FieldCount; i++)
        {
            var name = dataReader.GetName(i);
            var type = dataReader.GetFieldType(i);
            nameTypes.Add(new KeyValuePair<string, string>(dataReader.GetName(i), type.Name.Substring(0, 2)));
        }

        return nameTypes;
    }
}