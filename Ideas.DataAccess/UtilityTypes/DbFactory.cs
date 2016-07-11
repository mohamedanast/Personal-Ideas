using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.UtilityTypes
{
    public class DbFactory
    {
        public static IUnitOfWork GetUnitOfWork()
        {
            return new EFUnitOfWork(new IdeasContext());
        }
    }
}
