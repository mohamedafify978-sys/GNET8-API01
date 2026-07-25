using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity.product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specifications
{
    internal abstract class baseSpecificatin<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; private set; }

        public Expression<Func<TEntity, object>>? orderby { get; private set; }

        public Expression<Func<TEntity, object>>? orderbyDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pagesize , int pageindex)
        {
            IsPaginated = true;
            Take = pagesize;
            Skip = (pageindex -1) * pagesize;
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderbyExpression)
        {
             orderby = orderbyExpression;


        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderbyDescExpression)
        {
            orderbyDescending = orderbyDescExpression;
        }
        //protected void AddCondition(Expression<Func<TEntity, bool>> condition)
        //{
        //    condition = Criteria;

        //}
        protected baseSpecificatin(Expression<Func<TEntity, bool>> Criteria)
        {
            this.Criteria = Criteria;
            
        }
        protected void AddInclude(Expression<Func<TEntity, object>> include)
        {
            IncludeExpressions.Add(include);
        }
    }
}
