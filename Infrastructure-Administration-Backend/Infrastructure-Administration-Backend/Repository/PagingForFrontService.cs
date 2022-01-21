using Infrastructure_Administration_Backend.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure_Administration_Backend.Repository;
using System.Threading.Tasks;
using PagedList.Core;

namespace Infrastructure_Administration_Backend.Repository
{
    public class PagingForFrontService
    {
        private readonly IFilterRepository filt;

        public PagingForFrontService(IFilterRepository filt) => this.filt = filt;

        public ExportPagingModelForFront FilterPagingMethod(FilterModel model)
        {

            var res = filt.FilterMethod(model);

            int pageNumber = 1;

            int pageSize = 10;

              var result = res.ToPagedList(pageNumber, pageSize);


            var exportModelForFront = new ExportPagingModelForFront()
            {
                Total = result.TotalItemCount,
                PageSize = result.PageSize,
                PageCount = result.PageCount,
                Page = result.PageNumber,

                Items = result
            };

             return exportModelForFront;
        }

    }
}
