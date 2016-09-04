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
            ApplicationViewModel appVM = new ApplicationViewModel();
            ViewModel dashboardVM = CreateDashboardVM(appVM);
            appVM.CurrentPageVM = dashboardVM;

            ViewModel quickLinksVM = new QuickLinksViewModel();
            quickLinksVM.RootVM = appVM;
            appVM.QuickLinksVM = quickLinksVM;

            appVM.NotificationVM = new NotificationViewModel();

            return appVM;
        }

        public static ViewModel CreateDashboardVM(ViewModel appVM)
        {
            ViewModel ideasVM = CreateIdeasVM();
            ViewModel fruitfulIdeasVM = CreateFruitfulIdeasVM();
            ViewModel popularTagsVM = CreatePopularTagsVM();

            ViewModel dashboardVM = new DashboardViewModel(ideasVM, fruitfulIdeasVM, popularTagsVM);
            dashboardVM.RootVM = appVM;

            ideasVM.RootVM = appVM;
            ideasVM.ParentVM = dashboardVM;
            fruitfulIdeasVM.RootVM = appVM;
            fruitfulIdeasVM.ParentVM = dashboardVM;
            popularTagsVM.RootVM = appVM;
            popularTagsVM.ParentVM = dashboardVM;

            return dashboardVM;
        }

        private static ViewModel CreatePopularTagsVM()
        {
            ViewModel tagsVM = new PopularTagsViewModel();

            return tagsVM;
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
            ViewModel ideasVM = new ManageIdeasViewModel();
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
