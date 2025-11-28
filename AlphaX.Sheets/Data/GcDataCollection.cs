using System.Collections;
using System.Data;
using System.Reflection;

namespace AlphaX.Sheets.Data;

internal class AlphaXDataCollection : IDataCollection
{
    private object _actualSource;

    private Dictionary<string, PropertyInfo> _itemPropertyInfo;
    public DataSourceType DataSourceType { get; private set; }

    public int Count
    {
        get
        {
            switch(DataSourceType)
            {
                case DataSourceType.IList:
                    return (_actualSource as IList).Count;

                case DataSourceType.IEnumerable:
                    return 0;

                case DataSourceType.DataTable:
                    return (_actualSource as DataTable).Rows.Count;

                default:
                    return 0;
            }
        }
    }

    public object ActualSource => _actualSource;

    public AlphaXDataCollection(object source)
    {
        _actualSource = source;
        InitCollection();
    }

    private void InitCollection()
    {
        if (_actualSource is IList list)
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
        else if(_actualSource is IEnumerable enumerable)
        {
            DataSourceType = DataSourceType.IEnumerable;
        }
        else
        {
            DataSourceType = _actualSource is DataTable table ? DataSourceType.DataTable : DataSourceType.NotSupported;
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
                return (_actualSource as IList)[index];

            case DataSourceType.IEnumerable:
                int currentIndex = 0;
                var enumerator = (_actualSource as IEnumerable).GetEnumerator();
                do
                {
                    if (index == currentIndex)
                        break;

                    currentIndex++;                      
                }
                while (enumerator.MoveNext());               
                return enumerator.Current;

            case DataSourceType.DataTable:
                return (_actualSource as DataTable).Rows[index];

            default:
                return null;
        }
    }

    public PropertyInfo GetPropertyInfo(string name) => _itemPropertyInfo[name];

    public void Dispose()
    {
        _actualSource = null;
        _itemPropertyInfo = null;
    }
}
