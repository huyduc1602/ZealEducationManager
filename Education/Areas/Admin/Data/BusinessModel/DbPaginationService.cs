using System;
using Education.Areas.Admin.Data.BusinessModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data.BusinessModel
{
    public class DbPaginationService : IPaginationService
    {
        public Pagination getInfoPaginate(int totalData, int limit, int currentPage)
        {
            Pagination pagination = new Pagination();
            pagination.Limit = limit;

            int total = setInfoTotalPage(totalData, limit);
            pagination.TotalPage = total;
            pagination.CurrentPage = checkCurentPage(currentPage, total);

            int start = findStart(pagination.CurrentPage, limit);
            pagination.StartModel = start;
            int end = findEnd(start, limit, totalData);
            pagination.EndModel = end;

            return pagination;
        }
        private int findEnd(int start, int limit, int totalData)
        {
            int end = 0;
            if ((start + limit) > totalData)
            {
                end = totalData;
            }
            else
            {
                end = start + limit - 1;
            }
            return end;
        }

        private int findStart(int currentPage, int limit)
        {

            return ((currentPage - 1) * limit);
        }

        private int setInfoTotalPage(int totalData, int limit)
        {
            int totalPage = 0;
            if (totalData % limit == 0)
            {
                totalPage = (int)(totalData / limit);
            }
            else
            {
                totalPage = (int)(totalData / limit) + 1;
            }
            return totalPage;
        }

        public int checkCurentPage(int currentPage, int totalPage)
        {
            if (currentPage < 1)
            {
                return 1;
            }
            if (currentPage > totalPage)
            {
                return totalPage;
            }
            return currentPage;
        }
    }
}