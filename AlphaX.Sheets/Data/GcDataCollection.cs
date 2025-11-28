using System.Collections;
using System.Data;
using System.Reflection;

namespace AlphaX.Sheets.Data;

internal class AlphaXDataCollection : IDataCollection
{
    private Dictionary<string, PropertyInfo> _itemPropertyInfo;
    public DataSourceType DataSourceType { get; private set; }

    public int Count => DataSourceType switch
    {
        DataSourceType.IList => (ActualSource as IList).Count,
        DataSourceType.IEnumerable => 0,
        DataSourceType.DataTable => (ActualSource as DataTable).Rows.Count,
        _ => 0,
    };

    public object ActualSource { get; private set; }

    public AlphaXDataCollection(object source)
    {
        ActualSource = source;
        InitCollection();
    }

    private void InitCollection()
    {
        if (ActualSource is IList list)
        {
            var type = GetItemType(list);

            if (type != null)
            {
                var properties = type.GetProperties();
                _itemPropertyInfo = [];

                foreach (var property in properties)
                {
                    _itemPropertyInfo.Add(property.Name, property);
                }

                DataSourceType = DataSourceType.IList;
            }
        }
        else
        {
            DataSourceType = ActualSource is IEnumerable enumerable
                ? DataSourceType.IEnumerable
                : ActualSource is DataTable ? DataSourceType.DataTable : DataSourceType.NotSupported;
        }
    }

    private Type GetItemType(IList list)
    {
        var enumerable_type =
                list.GetType()
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GenericTypeArguments.Length == 1)
                .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

        return enumerable_type != null ? enumerable_type.GenericTypeArguments[0] : list.Count == 0 ? null : list[0].GetType();
    }

    public object GetItemAt(int index)
    {
        switch (DataSourceType)
        {
            case DataSourceType.IList:
                return (ActualSource as IList)[index];

            case DataSourceType.IEnumerable:
                var currentIndex = 0;
                var enumerator = (ActualSource as IEnumerable).GetEnumerator();
                do
                {
                    if (index == currentIndex)
                        break;

                    currentIndex++;
                }
                while (enumerator.MoveNext());
                return enumerator.Current;

            case DataSourceType.DataTable:
                return (ActualSource as DataTable).Rows[index];

            default:
                return null;
        }
    }

    public PropertyInfo GetPropertyInfo(string name) => _itemPropertyInfo[name];

    public void Dispose()
    {
        ActualSource = null;
        _itemPropertyInfo = null;
    }
}
