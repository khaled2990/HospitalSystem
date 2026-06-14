using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class QueryParameters
    {
        private const int MaxPageSize = 10;
        private const int DefaultPageSize = 5; 
        public string? Search { get; set; }
        public SortBy SortBy { get; set; }
        public int PageIndex { get; set; } = 1;
        private int pageSize=5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? DefaultPageSize : value; }
        }

    }
}
