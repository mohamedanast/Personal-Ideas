using Ideas.DataAccess.Entities;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ideas.Utilities
{
    public static class ViewFactory
    {
        public static ViewModel CreateStartVM()
        {
            ViewModel dashboardVM = CreateDashboardVM();
            ViewModel appVM = new ApplicationViewModel(dashboardVM);

            return appVM;
        }

        public static ViewModel CreateDashboardVM()
        {
            ViewModel ideasVM = CreateIdeasVM();
            ViewModel fruitfulIdeasVM = CreateFruitfulIdeasVM();
            ViewModel dashboardVM = new DashboardViewModel(ideasVM, fruitfulIdeasVM);

            return dashboardVM;
        }

        public static ViewModel CreateIdeasVM()
        {
            ViewModel ideasVM = new IdeasViewModel();

            return ideasVM;
        }

        public static ViewModel CreateFruitfulIdeasVM()
        {
            ViewModel ideasVM = new FruitfuldeasViewModel();

            return ideasVM;
        }

        public static ViewModel CreateIdeaVM(bool isEdit, Idea currentIdea)
        {
            IdeaViewModel ideaVM = new IdeaViewModel(isEdit);
            if (currentIdea != null)
            {
                ideaVM.CurrentIdea = currentIdea;
                ideaVM.IdeaId = currentIdea.IdeaId;
            }
            else
            {
                ideaVM.CurrentIdea = new Idea();
            }

            return ideaVM;
        }
    }
}
