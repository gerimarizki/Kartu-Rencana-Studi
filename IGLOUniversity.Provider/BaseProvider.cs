using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGLOUniversity.Provider
{
    public class BaseProvider
    {
        protected const int dataPerPage = 10;

        protected int GetSkipValue(int page)
        {
            return (page - 1) * dataPerPage;
        }

        protected int GetTotalPage(int totalData)
        {
            return (int)Math.Ceiling(totalData / (decimal)dataPerPage);
        }
    }
}
