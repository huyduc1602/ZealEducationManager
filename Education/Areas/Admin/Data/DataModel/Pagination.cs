using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Education.Areas.Admin.Data
{
    public class Pagination
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int StartModel { get; set; }
        public int EndModel { get; set; }
        public int Limit { get; set; }

        public Pagination()
        {
        }

        public Pagination(int currentPage, int totalPage, int startModel, int endModel, int limit)
        {
            CurrentPage = currentPage;
            TotalPage = totalPage;
            StartModel = startModel;
            EndModel = endModel;
            Limit = limit;
        }
    }
}