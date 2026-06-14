using DomainLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class BaseSpecification<Entity> : ISpecification<Entity> where Entity : class 
    {
        public BaseSpecification(Expression<Func<Entity, bool>> expression) {
        Where = expression;
        }
        public Expression<Func<Entity, bool>>? Where  { get; private set; }
     
        public Expression<Func<Entity, object>>? OrderByAsc { get; private set; }

        public Expression<Func<Entity, object>>? OrderByDesc { get; private set; }

        public void orderByAsc(Expression<Func<Entity, object>> expression)
        {
            OrderByAsc = expression;
        }
        public void orderByDesc(Expression<Func<Entity, object>> expression)
        {
            OrderByDesc = expression;
        }
       
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public void Pagination(int pageSize, int pageIndex)
        {
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
        }
    }
}
