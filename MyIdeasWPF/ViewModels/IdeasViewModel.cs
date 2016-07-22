using Ideas.DataAccess.BaseTypes;
using Ideas.DataAccess.Entities;
using Ideas.DataAccess.Model;
using Ideas.DataAccess.UtilityTypes;
using Ideas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ideas.ViewModels
{
    public class IdeasViewModel : ViewModel
    {
        protected int ideaId;
        protected IList<Idea> ideas;
        private ICommand getIdeasCmd;
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

        protected virtual void GetIdeas()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                ideas = transaction.IdeaRepo.GetByQuery(i => i.Status != (int)IdeaStatus.Archived ).ToList();
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

        public ICommand DeleteIdeaCommand
        {
            get
            {
                if (deleteIdeaCmd == null)
                    deleteIdeaCmd = new ActionCommand(e => DeleteIdea(), c => IdeaId > 0);

                return deleteIdeaCmd;
            }
        }
    }
}
