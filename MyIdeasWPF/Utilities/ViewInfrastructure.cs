using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ideas.Utilities
{
    class ViewInfrastructure<TView> where TView : ContentControl
    {
        public ViewModel ViewModel { get; private set; }
        public TView View { get; private set; }

        public ViewInfrastructure(TView view, ViewModel viewModel)
        {
            this.View = view;
            this.ViewModel = viewModel;
        }
    }
}
