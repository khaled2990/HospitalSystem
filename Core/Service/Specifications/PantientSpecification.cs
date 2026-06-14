using DomainLayer;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class PantientSpecification : BaseSpecification<Patient>
    {
        public PantientSpecification(QueryParameterPatient query) : base(
            (x => (string.IsNullOrEmpty(query.Search) || x.Name.ToLower().Contains(query.Search.ToLower())))
            )
        {
            Pagination(query.PageSize, query.PageIndex);
            var sort = query.SortBy;
            switch (sort)
            {
                case SortPatient.NameAsc:
                    orderByAsc(x => x.Name); break;
                case SortPatient.NameDesc:
                    orderByDesc(x => x.Name); break ;
                case SortPatient.AgeAsc
                    : orderByAsc(x => x.Age); break ;
                case SortPatient.AgeDesc:
                    orderByDesc(x => x.Age); break;
                default:
                    break;

            }
        }
    }
}
