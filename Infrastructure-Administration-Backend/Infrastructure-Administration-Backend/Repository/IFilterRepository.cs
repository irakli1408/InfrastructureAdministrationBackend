using Infrastructure_Administration_Backend.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Infrastructure_Administration_Backend.Repository.FilterRepository;
//using static Infrastructure_Administration_Backend.Repository.FilterRepository;

namespace Infrastructure_Administration_Backend.Repository
{
    public interface IFilterRepository
    {
        public IQueryable<Ex> FilterMethod(FilterModel model);
    }
}
