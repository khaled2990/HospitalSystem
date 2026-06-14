using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.Specifications
{
    public static class ApplySpecification<Entity> where Entity : class
    {
       public static IQueryable<Entity> GetQuert(IQueryable<Entity> queryable, ISpecification<Entity> specification)
        {

            var query = queryable;
            if (specification.Where is not null)
            {
                query = query.Where(specification.Where);
            }
            if (specification.OrderByAsc is not null)
            {
                query= query.OrderBy(specification.OrderByAsc);
            }
            if (specification.OrderByDesc is not null)
            {
                query=query.OrderByDescending(specification.OrderByDesc);
            }
            query=query.Skip(specification.Skip).Take(specification.Take);   
            return query;
        }
    }
}
