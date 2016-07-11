﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ideas.ViewModels
{
    class ActionCommand : ICommand
    {
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;

        public ActionCommand(Action<object> execute): this(execute, null)
        { }

        public ActionCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        #region implement ICommand
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion
    }
}
