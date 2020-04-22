using System;
using System.Collections.Generic;
using System.Text;

namespace Pagination
{
    public class Paging
    {
        public int Page(int Entries, int pageSize)
        {
            int maxpPage = (Entries / pageSize) - (Entries % pageSize == 0 ? 1 : 0);
            return maxpPage;
        }
    }
}
