
//using PagedList;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure_Administration_Backend.Repository.FilterRepository;
//using static Infrastructure_Administration_Backend.Repository.FilterRepository;

namespace Infrastructure_Administration_Backend.DataModels
{
    public class ExportPagingModelForFront
    {
        public int Total { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int Page { get; set; }

        public IPagedList<Ex> Items { get; set; }
    }
}
