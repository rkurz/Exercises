using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSuiteExercise
{
    interface iLruCache
    {
        object Get(object key);
        void Put(object key, object value);
        int GetMaxSize();
        string ToString();
    }
}
