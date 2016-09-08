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
        private ICommand newIdeaCommand;

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

        public string NewIdeaLnkVisibility
        {
            get
            {
                ApplicationViewModel MainVM = this.RootVM as ApplicationViewModel;
                if (MainVM != null && MainVM.CurrentPageVM != null && MainVM.CurrentPageVM is IdeaViewModel)
                    return Constants.VisibilityCollapsed;

                return Constants.VisibilityVisible;
            }
            set
            {
                //Do nothing
                OnPropertyChanged("NewIdeaLnkVisibility");
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

        public override void Refresh()
        {
            this.BackLnkVisibility = Constants.VisibilityVisible;   
            this.IdeasLnkVisibility = Constants.VisibilityVisible;
            this.NewIdeaLnkVisibility = Constants.VisibilityVisible;
        }

        private void NewIdea()
        {
            ViewModel viewModel = ViewFactory.CreateIdeaVM(true, null, this.RootVM);
            viewModel.NavigateTo();
        }

        private void ViewAllIdeas()
        {
            ViewModel viewModel = ViewFactory.CreateAllIdeasVM(this.RootVM);
            viewModel.NavigateTo();
        }

        public ICommand NewIdeaCommand
        {
            get
            {
                if (newIdeaCommand == null)
                    newIdeaCommand = new ActionCommand(p => NewIdea());

                return newIdeaCommand;
            }
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
