using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.UtilityTypes
{
    public class PagedResult<TEntity>
    {
        IEnumerable<TEntity> items;
        int count;

        public PagedResult(IEnumerable<TEntity> items, int count)
        {
            this.items = items;
            this.count = count;
        }

        public IEnumerable<TEntity> Items { get { return items; } }
        public int TotalCount { get { return count; } }
    }
}
