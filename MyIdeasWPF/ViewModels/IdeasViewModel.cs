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
                if (ideas == null) GetIdeas();  // TODO: Just for testing, remove & use commands instead
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
                if (ideas == null) GetIdeas();  // TODO: Just for testing, remove & use commands instead
                IEnumerable<IdeaView> ideasViewData = ideas.Select(idea => new IdeaView
                {
                    IdeaId = idea.IdeaId,
                    Title = idea.Title,
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
                    return new IdeaView { IdeaId = selectedIdea.IdeaId, Title = selectedIdea.Title };
                else
                    return null;
            }
            set
            {
                selectedIdea = ideas.Where(i => i.IdeaId == value.IdeaId).FirstOrDefault();
                OnPropertyChanged("SelectedIdea");
            }
        }

        protected virtual void GetIdeas()
        {
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
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                transaction.IdeaRepo.DeleteByID(IdeaId);
                transaction.Commit();
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
                    deleteIdeaCmd = new ActionCommand(e => DeleteIdea(), c => IdeaId > 0);

                return deleteIdeaCmd;
            }
        }

        public class IdeaView
        {
            public int IdeaId { get; set; }
            public string Title { get; set; }
            public string SecondDisplayColumn { get; set; }
        }
    }
}
