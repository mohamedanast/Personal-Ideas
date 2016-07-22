using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.UtilityTypes
{
    public enum IdeaStatus
    {
        [Description("Conceived")]
        Conceived = 10,
        [Description("Prioritized")]
        Prioritized = 11,
        [Description("On Implementation")]
        OnImplementation = 12,
        [Description("Aready Implemented")]
        Implemented = 13,
        [Description("Archived")]
        Archived = 0
    }
}
