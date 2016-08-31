using Ideas.DataAccess.UtilityTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.UI.Utilities
{
    public static class UICommon
    {
        /// <summary>
        /// Get the IdeaStatus enumeration converted into a collection, with Descrition attribute defined for the enum member as values 
        /// </summary>
        public static Dictionary<string, string> GetStatusCollection(IdeaStatus refIdeaStatus, bool excludeReference)
        {
            Dictionary<string, string> StatusCollection = new Dictionary<string, string>();
            if (excludeReference)
            {
                foreach (string status in Enum.GetNames(refIdeaStatus.GetType()))
                {
                    if (status != refIdeaStatus.ToString())
                    {
                        FieldInfo fieldInfo = refIdeaStatus.GetType().GetField(status);
                        if (fieldInfo != null)
                        {
                            object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                            if (attributes.Length > 0)
                            {
                                StatusCollection.Add(status, ((DescriptionAttribute)attributes[0]).Description);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (string status in Enum.GetNames(refIdeaStatus.GetType()))
                {
                    FieldInfo fieldInfo = refIdeaStatus.GetType().GetField(status);
                    if (fieldInfo != null)
                    {
                        object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (attributes.Length > 0)
                        {
                            StatusCollection.Add(status, ((DescriptionAttribute)attributes[0]).Description);
                        }
                    }
                }
            }

            return StatusCollection;
        }
    }
}
