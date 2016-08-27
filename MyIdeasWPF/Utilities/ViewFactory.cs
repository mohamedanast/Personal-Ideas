using Ideas.DataAccess.Entities;
using Ideas.UI.Utilities;
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
            ViewModel appVM = new ApplicationViewModel();
            ViewModel dashboardVM = CreateDashboardVM(appVM);
            (appVM as ApplicationViewModel).CurrentPageVM = dashboardVM;

            return appVM;
        }

        public static ViewModel CreateDashboardVM(ViewModel appVM)
        {
            ViewModel ideasVM = CreateIdeasVM();
            ViewModel fruitfulIdeasVM = CreateFruitfulIdeasVM();

            ViewModel dashboardVM = new DashboardViewModel(ideasVM, fruitfulIdeasVM);
            dashboardVM.RootVM = appVM;

            ideasVM.RootVM = appVM;
            ideasVM.ParentVM = dashboardVM;
            fruitfulIdeasVM.RootVM = appVM;
            fruitfulIdeasVM.ParentVM = dashboardVM;

            return dashboardVM;
        }

        public static ViewModel CreateIdeasVM()
        {
            ViewModel ideasVM = new IdeasViewModel();

            return ideasVM;
        }

        public static ViewModel CreateFruitfulIdeasVM()
        {
            ViewModel ideasVM = new FruitfulIdeasViewModel();

            return ideasVM;
        }

        public static ViewModel CreateAllIdeasVM(ViewModel rootVM)
        {
            ViewModel ideasVM = new AllIdeasViewModel();
            ideasVM.RootVM = rootVM;
            ideasVM.SetNavReferenceVM(rootVM);

            return ideasVM;
        }

        public static ViewModel CreateIdeaVM(bool isEdit, Idea currentIdea, ViewModel rootVM)
        {
            IdeaViewModel ideaVM = null;
            if (currentIdea != null)
            {
                ideaVM = new IdeaViewModel(isEdit, currentIdea.IdeaId);
                ideaVM.CurrentIdea = currentIdea;
            }
            else
            {
                ideaVM = new IdeaViewModel(isEdit, null);
                ideaVM.CurrentIdea = new Idea();
            }

            ideaVM.RootVM = rootVM;
            ideaVM.SetNavReferenceVM(rootVM);

            return ideaVM;
        }
    }
}
