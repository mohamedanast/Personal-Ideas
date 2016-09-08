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

        public ViewModel PopularTagsVM
        {
            get; private set;
        }

        public DashboardViewModel(ViewModel ideasVM, ViewModel fruitfulIdeasVM, ViewModel popularTagsVM)
        {
            IdeasVM = ideasVM;
            FruitfulIdeasVM = fruitfulIdeasVM;
            PopularTagsVM = popularTagsVM;
        }

        public override void Refresh()
        {
            // Refresh the individual parts
            IdeasVM.Refresh();
            FruitfulIdeasVM.Refresh();
            PopularTagsVM.Refresh();
        }
    }
}
