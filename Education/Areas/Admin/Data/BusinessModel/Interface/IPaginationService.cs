using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Areas.Admin.Data.BusinessModel
{
    interface IPaginationService
    {
        Pagination getInfoPaginate(int totalData, int limit, int currentPage);
    }
}
