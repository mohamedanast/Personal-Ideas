using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Model;
using Ideas.DataAccess.UtilityTypes;
using Ideas.Utilities;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ideas.UI.Utilities;
using System.Globalization;

namespace Ideas.ViewModels
{
    public class IdeasViewModel : ViewModel
    {
        protected int ideaId;
        public Idea selectedIdea;
        protected IList<Idea> ideas;
        private ICommand getIdeasCmd;
        private ICommand editIdeaCmd;
        private ICommand deleteIdeaCmd;
        
        public int IdeaId
        {
            get { return ideaId; }
            set
            {
                if (value != ideaId)
                {
                    ideaId = value;
                    OnPropertyChanged("CurrentIdea");
                }
            }
        }

        public IList<Idea> Ideas
        {
            get {
                if (ideas == null) GetIdeas();
                return ideas; }
            set
            {
                if (value != ideas)
                {
                    ideas = value;
                    OnPropertyChanged("Ideas");
                }
            }
        }

        public virtual IEnumerable<IdeaView> IdeasViewData
        {
            get
            {
                if (ideas == null) GetIdeas();
                IEnumerable<IdeaView> ideasViewData = ideas.Select(idea => new IdeaView
                {
                    Idea = idea,
                    SecondDisplayColumn = idea.Created.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern)
                });

                return ideasViewData;
            }
        }
        
        public IdeaView SelectedIdea
        {
            get
            {
                if (selectedIdea != null)
                    return new IdeaView { Idea = selectedIdea };
                else
                    return null;
            }
            set
            {
                selectedIdea = value.Idea;
                OnPropertyChanged("SelectedIdea");
            }
        }

        public override void Refresh()
        {
            if (ideas != null)
            {
                ideas.Clear();
                Ideas = null;   // Force Dependency Prop reoad and also idea reload. The other checks and clearing can be on the private variable.
            }
        }

        protected virtual void GetIdeas()
        {
            // Retrieves latest ideas, overridden when other ideas are required
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                ideas = transaction.IdeaRepo.GetByQuery(i => i.Status != (int)IdeaStatus.Archived, ideas => ideas.OrderByDescending(i=> i.Created) ).Take(10).ToList();
            }
        }

        protected virtual void EditIdea()
        {
           if (selectedIdea != null)
            {
                ViewModel editIdeaVM = ViewFactory.CreateIdeaVM(true, selectedIdea, this.RootVM);
                editIdeaVM.NavigateTo();
            }
        }

        private void DeleteIdea()
        {
            try
            {
                using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
                {
                    selectedIdea.Status = (byte)IdeaStatus.Archived;
                    transaction.IdeaRepo.Update(selectedIdea);
                    transaction.Commit();
                    (this.RootVM as ApplicationViewModel).AddNotification("Idea Archived", "Idea '" + selectedIdea.Title + "' has been archived.");

                    // Refresh current view. (Todo: Refresh not working on ideas view immediately)
                    this.Refresh();
                    // Refresh dashboard. What if LastVM is not dashboard?
                    this.LastVM.Refresh();
                }
            }
            catch (Exception exception)
            {
                IdeaCommon.HandleError(this.RootVM, exception);
            }
        }

        public ICommand GetIdeasCommand
        {
            get
            {
                if (getIdeasCmd == null)
                    getIdeasCmd = new ActionCommand(p => GetIdeas());

                return getIdeasCmd;
            }
        }

        public ICommand EditIdeaCommand
        {
            get
            {
                if (editIdeaCmd == null)
                    editIdeaCmd = new ActionCommand(p => EditIdea());

                return editIdeaCmd;
            }
        }

        public ICommand DeleteIdeaCommand
        {
            get
            {
                if (deleteIdeaCmd == null)
                    deleteIdeaCmd = new ActionCommand(e => DeleteIdea());

                return deleteIdeaCmd;
            }
        }

        public class IdeaView
        {
            public Idea Idea { get; set; }
            public string SecondDisplayColumn { get; set; }
        }
    }
}
