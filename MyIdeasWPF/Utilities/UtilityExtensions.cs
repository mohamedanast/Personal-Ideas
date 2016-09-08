using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.UI.Utilities
{
    public static class UtilityExtensions
    {
        public static void NavigateTo(this ViewModel targetViewModel)
        {
            if (targetViewModel.RootVM != null && targetViewModel.RootVM is ApplicationViewModel)
            {
                ApplicationViewModel MainVM = targetViewModel.RootVM as ApplicationViewModel;
                MainVM.CurrentPageVM = targetViewModel;

                // Keep a copy of reference of last VM in quick links also
                MainVM.QuickLinksVM.LastVM = targetViewModel.LastVM;
                MainVM.QuickLinksVM.Refresh();
            }
        }

        public static void SetNavReferenceVM(this ViewModel current, ViewModel rootVM)
        {
            // Reference for navigating back
            if (rootVM is ApplicationViewModel)
                current.LastVM = (rootVM as ApplicationViewModel).CurrentPageVM;
        }
    }
}
