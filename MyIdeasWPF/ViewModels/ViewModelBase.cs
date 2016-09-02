using Ideas.UI.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ideas.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }

        //[DebuggerStepThrough]
        public virtual void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new Exception("Invalid property name: " + propertyName);
        }

        // Navigation Properties
        public ViewModel RootVM { get; set; }
        public ViewModel LastVM { get; set; }
        public ViewModel ParentVM { get; set; }
        private ICommand cancelCommand;

        protected virtual void CancelAction()
        {
            if (RootVM != null && LastVM != null)
            {
                LastVM.NavigateTo();
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                    cancelCommand = new ActionCommand(p => CancelAction());

                return cancelCommand;
            }
        }
    }
}
