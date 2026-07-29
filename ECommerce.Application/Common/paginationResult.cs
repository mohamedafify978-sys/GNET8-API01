using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public sealed  class paginationResult<TEntity>
    {
        public paginationResult(int pageIndex, int pageSize, int count, IReadOnlyList<TEntity> data)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int pageIndex { get; }
        public int pageSize { get; }

        public int Count { get; }
        public IReadOnlyList<TEntity> Data {  get; set; }
    }
}
