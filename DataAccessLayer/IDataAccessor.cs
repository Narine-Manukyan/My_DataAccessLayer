using System.Collections.Generic;

namespace DataAccessLayer
{
    /// <summary>
    /// For accessing database data without using ADO.Net specific code in.
    /// </summary>
    public interface IDataAccessor
    {
        /// <summary>
        /// For executing and retrieving data for a given operation and input parameters.
        /// </summary>
        /// <param name="code">A code which specifies mapped name of the operation to be executed.</param>
        /// <param name="parameters">
        /// Key is the parameter’s name...value is the parameter’s value.
        /// </param>
        /// <returns>IEnumerable<T></returns>
        IEnumerable<object> GetData(string code, List<KeyValuePair<string, object>> parameters);
    }
}
