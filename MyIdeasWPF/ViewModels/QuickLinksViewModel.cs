using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Model;
using Ideas.DataAccess.UtilityTypes;
using Ideas.Utilities;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ideas.UI.Utilities;
using System.Globalization;

namespace Ideas.ViewModels
{
    public class QuickLinksViewModel : ViewModel
    {
        private ICommand viewAllIdeasCmd;

        public string BackLnkVisibility
        {
            get
            {
                ApplicationViewModel MainVM = this.RootVM as ApplicationViewModel;
                if (MainVM != null && MainVM.CurrentPageVM != null && MainVM.CurrentPageVM is DashboardViewModel)
                    return Constants.VisibilityCollapsed;

                return Constants.VisibilityVisible;
            }
            set
            {
                //Do nothing
                OnPropertyChanged("BackLnkVisibility");
            }
        }


        public string IdeasLnkVisibility
        {
            get
            {
                ApplicationViewModel MainVM = this.RootVM as ApplicationViewModel;
                if (MainVM != null && MainVM.CurrentPageVM != null && MainVM.CurrentPageVM is ManageIdeasViewModel)
                    return Constants.VisibilityCollapsed;

                return Constants.VisibilityVisible;
            }
            set
            {
                //Do nothing
                OnPropertyChanged("IdeasLnkVisibility");
            }
        }


        private void ViewAllIdeas()
        {
            ViewModel viewModel = ViewFactory.CreateAllIdeasVM(this.RootVM);
            viewModel.NavigateTo();
        }

        public ICommand ViewAllIdeasCommand
        {
            get
            {
                if (viewAllIdeasCmd == null)
                    viewAllIdeasCmd = new ActionCommand(p => ViewAllIdeas());

                return viewAllIdeasCmd;
            }
        }
    }
}
