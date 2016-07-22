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
using System.Reflection;
using System.ComponentModel;

namespace Ideas.ViewModels
{
    public class IdeaViewModel : ViewModel
    {
        private int? ideaId;
        private bool isEdit;
        private IDictionary<string, string> StatusCollection;
        private IDictionary<string, string> AllTags;
        private Idea currentIdea;
        private ICommand getIdeaCmd;
        private ICommand saveIdeaCmd;

        public IdeaViewModel(bool isEdit)
        {
            // Get the IdeaStatus enum as a collection, so that the view can bind to it without hardcoding the statuses.
            StatusCollection = new Dictionary<string, string>();
            IdeaStatus archived = IdeaStatus.Archived;
            foreach (string status in Enum.GetNames(archived.GetType()))
            {
                if (status != archived.ToString())
                {
                    FieldInfo fieldInfo = archived.GetType().GetField(status);
                    if (fieldInfo != null)
                    {
                        object[] attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (attributes.Length > 0)
                        {
                            StatusCollection.Add(status, ((DescriptionAttribute)attributes[0]).Description);
                        }
                    }
                }
            }

            this.IsEdit = isEdit;
        }

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

        public int? IdeaId
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

        public bool IsEdit
        {
            get { return isEdit; }
            set
            {
                isEdit = value;
                if (isEdit) Visibility = "Visible"; else Visibility = "Collapsed";
            }
        }
        
        public string Visibility { get; private set; }

        public ICollection<string> StatusChoices
        {
            get { return StatusCollection.Values; }
        }

        public string CurrentStatus
        {
            get
            {
                if (IdeaId.HasValue && currentIdea != null)
                    return StatusCollection[((IdeaStatus)currentIdea.Status).ToString()];
                return null;
            }
            set
            {
                string statusName = StatusCollection.First(d => d.Value.Equals(value)).Key;
                IdeaStatus status = (IdeaStatus)Enum.Parse(((IdeaStatus)currentIdea.Status).GetType(), statusName);
                currentIdea.Status = (byte)status;
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
            if (this.IdeaId != null)
            {
                using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
                {
                    CurrentIdea = transaction.IdeaRepo.GetById(this.IdeaId.Value);
                }
            }
        }
        
        private void SaveIdea()
        {
            using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
            {
                if (IdeaId.HasValue)
                    transaction.IdeaRepo.Update(CurrentIdea);
                else
                    transaction.IdeaRepo.Insert(CurrentIdea);

                transaction.Commit();
            }
        }
    }
}
