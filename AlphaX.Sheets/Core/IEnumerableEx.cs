using System.Collections.Generic;

namespace AlphaX.Sheets.Core
{
    public interface IEnumerableEx<T>
    {
        /// <summary>
        /// Enumerates the items present in this collection.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Enumerate();
    }
}
