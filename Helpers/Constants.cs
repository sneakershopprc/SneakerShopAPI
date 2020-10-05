using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Helpers
{
    public class Constants
    {
        public struct SortBy
        {
            public const int SORT_DEFAULT = 0;
            public const int SORT_NAME_ASC = 1;
            public const int SORT_NAME_DES = 2;
        }

        public struct Status
        {
            public const string STATUS_WAITING = "Waiting";
            public const string STATUS_CANCEL = "Cancel";
            public const string STATUS_FINISH = "Finished";
        }

    }
}
