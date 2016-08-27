using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ideas.DataAccess.Entities;
using System.Windows.Input;
using Ideas.DataAccess.Model;
using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.UtilityTypes;
using Ideas.Utilities;
using Ideas.UI.Utilities;

namespace Ideas.ViewModels
{
    public class ApplicationViewModel : ViewModel
    {
        private ViewModel currentPageVM;
        private ICommand newIdeaCommand;
        private ICommand viewAllIdeasCmd;

        public ViewModel CurrentPageVM
        {
            get { return currentPageVM; }
            set
            {
                if (value != currentPageVM)
                {
                    currentPageVM = value;
                    OnPropertyChanged("CurrentPageVM");
                }
            }
        }
        
        private void NewIdea()
        {
            ViewModel viewModel = ViewFactory.CreateIdeaVM(true, null, this);
            viewModel.NavigateTo();
        }

        private void ViewAllIdeas()
        {
            ViewModel viewModel = ViewFactory.CreateAllIdeasVM(this);
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
