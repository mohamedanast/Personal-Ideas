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
    public class IdeaViewModel : ViewModel
    {
        private int ideaId;
        private Idea currentIdea;
        private ICommand getIdeaCmd;
        private ICommand saveIdeaCmd;
        
        public Idea CurrentIdea
        {
            get { return currentIdea; }
            set
            {
                if (value != currentIdea)
                {
                    currentIdea = value;
                    OnPropertyChanged("CurrentIdea");
                }
            }
        }

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

        public ICommand GetIdeaCommand
        {
            get
            {
                if (getIdeaCmd == null)
                    getIdeaCmd = new ActionCommand(p => GetIdea(), p => ideaId > 0);

                return getIdeaCmd;
            }
        }

        public ICommand SaveIdeaCommand
        {
            get
            {
                if (saveIdeaCmd == null)
                    saveIdeaCmd = new ActionCommand(p => SaveIdea());

                return saveIdeaCmd;
            }
        }

        private void GetIdea()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                CurrentIdea = transaction.IdeaRepo.GetById(this.IdeaId);
            }
        }

        //TODO: Insert Ideas
        private void SaveIdea()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                transaction.IdeaRepo.Update(CurrentIdea);
                transaction.Commit();
            }
        }
    }
}
