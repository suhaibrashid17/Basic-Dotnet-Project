using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helper;

namespace api.Interface
{
    public interface ISearchRepository
    {
        Task<object> SearchAsync(SearchCriteriaAdmin criteria);
    }
}