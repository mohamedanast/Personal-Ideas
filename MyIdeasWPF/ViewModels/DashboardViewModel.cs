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

namespace Ideas.ViewModels
{
    public class DashboardViewModel : ViewModel
    {   
        public ViewModel IdeasVM
        {
            get; private set;
        }

        public ViewModel FruitfulIdeasVM
        {
            get; private set;
        }

        public string TestString { get { return "Test string from Dashboard binding"; } }

        public DashboardViewModel(ViewModel ideasVM, ViewModel fruitfulIdeasVM)
        {
            IdeasVM = ideasVM;
            FruitfulIdeasVM = fruitfulIdeasVM;
        }
    }
}
