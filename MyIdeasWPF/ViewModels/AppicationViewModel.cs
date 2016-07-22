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

namespace Ideas.ViewModels
{
    public class ApplicationViewModel : ViewModel
    {
        private ViewModel currentPageVM;
        private ICommand newIdeaCommand;

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

        public ApplicationViewModel(ViewModel currentVM)
        {
            currentPageVM = currentVM;
        }

        private void NewIdea()
        {
            CurrentPageVM = ViewFactory.CreateIdeaVM(true, null);
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

    }
}
