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
        private ICommand cancelCommand;
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

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                    cancelCommand = new ActionCommand(p => CancelAction());

                return cancelCommand;
            }
        }

        protected virtual void CancelAction()
        {
            //TODO:
            System.Windows.MessageBox.Show("Not implemented");
        }
    }
}
