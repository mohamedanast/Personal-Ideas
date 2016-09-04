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
using System.Transactions;
using System.Collections.ObjectModel;
using Ideas.UI.Utilities;

namespace Ideas.ViewModels
{
    public class NotificationViewModel : ViewModel
    {
        private ICommand dismissCmd;
        private string notificationHeader;
        private string notificationText;
        private string visibility;

        public NotificationViewModel()
        {
            Visibility = Constants.VisibilityCollapsed;
        }
        
        public String NotificationHeader
        {
            get { return notificationHeader; }
            set
            {
                if (value != notificationHeader)
                {
                    notificationHeader = value;
                    OnPropertyChanged("NotificationHeader");
                }
            }
        }

        public String NotificationText
        {
            get { return notificationText; }
            set
            {
                if (value != notificationText)
                {
                    notificationText = value;
                    OnPropertyChanged("NotificationText");
                }
            }
        }

        public String Visibility
        {
            get { return visibility; }
            set
            {
                if (value != visibility)
                {
                    visibility = value;
                    OnPropertyChanged("Visibility");
                }
            }
        }

        private void DismissNotification()
        {
            Visibility = Constants.VisibilityCollapsed;
        }

        public ICommand DismissCmd
        {
            get
            {
                if (dismissCmd == null)
                    dismissCmd = new ActionCommand(p => DismissNotification());

                return dismissCmd;
            }
        }
    }
}
