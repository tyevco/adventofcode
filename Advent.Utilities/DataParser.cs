using System;
using System.Collections.Generic;

namespace Advent.Utilities
{
    public abstract class DataParser<T>
    {
        public T ParseData(string fileName)
        {
            IList<string> data = null;
            try
            {
                data = System.IO.File.ReadAllLines(fileName);

                return DeserializeData(data);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected abstract T DeserializeData(IList<string> data);
    }
}
