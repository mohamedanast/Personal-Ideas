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
        private ViewModel quickLinksVM;
        private NotificationViewModel notificationVM;
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

        public ViewModel QuickLinksVM
        {
            get { return quickLinksVM; }
            set
            {
                if (value != quickLinksVM)
                {
                    quickLinksVM = value;
                    OnPropertyChanged("QuickLinksVM");
                }
            }
        }

        public NotificationViewModel NotificationVM
        {
            get { return notificationVM; }
            set
            {
                if (value != notificationVM)
                {
                    notificationVM = value;
                    OnPropertyChanged("NotificationVM");
                }
            }
        }

        public void AddNotification(string header, string text)
        {
            this.NotificationVM.NotificationHeader = header;
            this.NotificationVM.NotificationText = text;
            this.NotificationVM.Visibility = Constants.VisibilityVisible;
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
