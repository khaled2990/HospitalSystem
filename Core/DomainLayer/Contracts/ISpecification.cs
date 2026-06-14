using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface ISpecification<Entity> where Entity : class
    {
        public Expression<Func<Entity, bool>>? Where { get; }
      
        public Expression<Func<Entity, object>>? OrderByAsc { get; }
        public Expression<Func<Entity, object>>? OrderByDesc { get; }
        public int Take { get; }
        public int Skip { get; }
    }
}
