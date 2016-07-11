using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.BaseTypes
{
    public abstract class EntityBase : ISetProperty, IValidatableObject
    {
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        private Dictionary<string, Func<object, IEnumerable<string>>> _ValidateMethods = new Dictionary<string, Func<object, IEnumerable<string>>>();

        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0); 
            }
        }

        public int ErrorCount
        {
            get { return errors.Count; }
        }
        
        public bool NoErrors
        {
            get { return (errors.Count == 0); }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// to be invoked when a prop changes and client needs to be notified
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));

            RaisePropertyChanged("HasErrors");
            RaisePropertyChanged("NoErrors");
            RaisePropertyChanged("ErrorCount");
        }

        public void AddValidationMethod(string propertyName, Func<object, IEnumerable<string>> method)
        {
            _ValidateMethods.Add(propertyName, method);
        }

        private bool ValidateProperty<T>(string propertyName, T newValue)
        {
            Func<object, IEnumerable<string>> validator = null;

            if (this._ValidateMethods.TryGetValue(propertyName, out validator))
            {
                IEnumerable<string> results = validator(newValue);
                SetErrors(propertyName, validator(newValue));

                if (results == null)
                    return true;
                else
                    return false;
            }
            else
                throw new MissingMethodException("No validation method is added to the validation dictionary for " + propertyName + " property.");
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
                return errors[propertyName];
            return null;
        }

        protected void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            if (errors == null)
            {
                //Clean up all errors for this property if the enumeration is empty
                if (this.errors.ContainsKey(propertyName))
                {
                    this.errors.Remove(propertyName);
                    RaiseErrorsChanged(propertyName);
                }
            }
            else
            {
                //Create a new entry for the property and add the error list
                if (!this.errors.ContainsKey(propertyName))
                    this.errors.Add(propertyName, new List<string>(errors));
                else
                {
                    //Replace the whole error list with the new one
                    this.errors[propertyName] = new List<string>(errors);
                }

                RaiseErrorsChanged(propertyName);
            }
        }

        public void SetProperty<T>(string propertyName, ref T backingField, T newValue)
        {
            if (!object.Equals(backingField, newValue))
            {
                if (ValidateProperty<T>(propertyName, newValue))
                {
                    backingField = newValue;
                    RaisePropertyChanged(propertyName);
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ICollection<ValidationResult> res = new List<ValidationResult>();
            res.Add(ValidationResult.Success);
            return res;
        }

        protected abstract void RegisterValidationMethods();
        
        protected abstract void ResetProperties();
    }
}
