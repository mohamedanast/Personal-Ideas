using Ideas.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.UI.Utilities
{
    public class WeightedTag
    {
        public Tag Tag { get; set; }
        public int Count { get; set; }
        public int Weight { get; set; }
    }
}
