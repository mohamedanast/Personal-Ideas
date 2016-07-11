using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.DataAccess.BaseTypes
{
    interface ISetProperty: INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        /// Add a vaidation method for the entity.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="method"></param>
        void AddValidationMethod(string propertyName, Func<object, IEnumerable<string>> method);

        /// <summary>
        /// Inheriting classes should call this method to set their properties so that property notification and
        /// data error notification can be provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="backingField"></param>
        /// <param name="newValue"></param>
        void SetProperty<T>(string propertyName, ref T backingField, T newValue);
    }
}
