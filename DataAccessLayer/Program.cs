using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            MyDAL dal = new MyDAL(@"C: \Users\Nar\Desktop\DataAccessLayer\mySqlQuery.txt");
            dal.GetData("_myQuery", null);

            var list = new List<KeyValuePair<string, object>>();
            list.Add(new KeyValuePair<string, object>("@BusinessEntityID", 7));
            dal.GetData("_sp", list);
        }
    }
}
