using System;

namespace Dane
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
