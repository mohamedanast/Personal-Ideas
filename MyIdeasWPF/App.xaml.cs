using Ideas.UI.Views;
using Ideas.Utilities;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ideas.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModel appVM = ViewFactory.CreateStartVM();
            MainWindow app = new MainWindow();
            app.DataContext = appVM;
            app.Show();
        }
    }
}
