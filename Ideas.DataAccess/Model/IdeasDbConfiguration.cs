using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.Model
{
    // Not required, but just a paceholder now for any seeding or configuration to be done later.
    class IdeasDbConfiguration : DbConfiguration
    {
        public IdeasDbConfiguration()
        {
            SetDatabaseInitializer<IdeasContext>(new CreateDatabaseIfNotExists<IdeasContext>());
        }
    }
}
