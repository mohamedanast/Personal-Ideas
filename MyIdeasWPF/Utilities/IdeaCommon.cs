using Ideas.DataAccess.UtilityTypes;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.UI.Utilities
{
    public static class IdeaCommon
    {
        /// <summary>
        /// Get the IdeaStatus enumeration converted into a collection, with Descrition attribute defined for the enum member as values 
        /// TODO: Good candidate for static property or caching
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

        public static void HandleError(ViewModel RootVM, Exception exception)
        {
            ApplicationViewModel AppVM = RootVM as ApplicationViewModel;
            AppVM.AddNotification("Error", exception.Message);

            //TODO: Log exception
        }
    }
}
