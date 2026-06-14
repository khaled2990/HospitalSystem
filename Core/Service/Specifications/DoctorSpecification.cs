using DomainLayer;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class DoctorSpecification : BaseSpecification<Doctor>
    {
        public DoctorSpecification(QueryParameters query) : base
            (p => string.IsNullOrWhiteSpace(query.Search) ||
            p.Name.ToLower().Contains(query.Search.ToLower()))
        {

        
            var sort = query.SortBy;
            switch (sort)
            {
                case SortBy.NameAsc:
                    orderByAsc(d => d.Name); break;
                case SortBy.NameDesc:
                    orderByDesc(d => d.Name); break;
                case SortBy.PriceAsc:
                    orderByAsc(d => d.Price); break;
                case SortBy.PriceDesc:
                    orderByDesc(d => d.Price); break;
                default:
                    break;
            }
            Pagination(query.PageSize, query.PageIndex);





        }

    }
}
