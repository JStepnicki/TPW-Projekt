using System;

namespace Data

{
    public abstract class AbstractDataApi
    {
        public static AbstractDataApi CreateApi()
        {
            return new DataApi();
        }

        internal sealed class DataApi : AbstractDataApi
        {

        }
    }
}
