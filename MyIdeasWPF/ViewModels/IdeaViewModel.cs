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
using System.Transactions;
using System.Collections.ObjectModel;
using Ideas.UI.Utilities;

namespace Ideas.ViewModels
{
    public class IdeaViewModel : ViewModel
    {
        private int? ideaId;
        private bool isEdit;
        private IDictionary<string, string> StatusCollection;
        private IDictionary<int, string> AllTags;
        private IDictionary<int, string> DeletedTags;
        private IDictionary<string, int?> CurrentTags;
        private Idea currentIdea;
        private ICommand associateTagCmd;
        private ICommand disAssociateTagCmd;
        private ICommand getIdeaCmd;
        private ICommand saveIdeaCmd;

        public IdeaViewModel(bool isEdit, int? iId)
        {
            this.IdeaId = iId;

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
            
            if (isEdit)
            {
                AllTags = new Dictionary<int, string>();
                using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
                {
                    IEnumerable<Tag> tags = transaction.TagRepo.GetByQuery();
                    foreach(Tag tag in tags)
                        AllTags.Add(tag.TagId, tag.TagName);
                }
            }

            CurrentTags = new Dictionary<string, int?>();
            if (ideaId != null)
            {
                //GetIdea(); Refresh required?

                using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
                {
                    IEnumerable<IdeaTag> tags = transaction.IdeaTagRepo.GetByQuery(it => it.IdeaId == ideaId);
                    foreach (IdeaTag tag in tags)
                        CurrentTags.Add(tag.Tag.TagName, tag.TagId);
                }
            }

            DeletedTags = new Dictionary<int, string>();
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

        public ObservableCollection<string> TagsSelected
        {
            get { return new ObservableCollection<string>(CurrentTags.Keys); }
        }

        public string TempStatusText
        {
            get; private set;
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

        public ICommand AssociateTagCommand
        {
            get
            {
                if (associateTagCmd == null)
                    associateTagCmd = new ActionCommand(p => AssociateTag(p));

                return associateTagCmd;
            }
        }

        public ICommand DisassociateTagCommand
        {
            get
            {
                if (disAssociateTagCmd == null)
                    disAssociateTagCmd = new ActionCommand(p => DisassociateTag(p));

                return disAssociateTagCmd;
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

        private void AssociateTag(object parameter)
        {
            string txtValue = parameter as string;
            if (!string.IsNullOrEmpty(txtValue ))
            {
                // Add new tags with null, to find and add to db upon saving
                if (CurrentTags.Keys.Count(k => k.Equals(txtValue, StringComparison.CurrentCultureIgnoreCase)) == 0)
                {
                    CurrentTags.Add(txtValue, null);
                    TempStatusText = string.Empty;
                    OnPropertyChanged("TagsSelected");
                    OnPropertyChanged("TempStatusText");
                }
            }
        }

        private void DisassociateTag(object parameter)
        {
            string txtValue = parameter as string;
            if (!string.IsNullOrEmpty(txtValue))
            {
                if (CurrentTags.Keys.Count(k => k.Equals(txtValue, StringComparison.CurrentCultureIgnoreCase)) == 1)
                {
                    KeyValuePair<string, int?> currentTag = CurrentTags.Where(t => t.Key.Equals(txtValue, StringComparison.CurrentCultureIgnoreCase)).Single();
                    if (currentTag.Value.HasValue)
                        DeletedTags.Add(currentTag.Value.Value, txtValue);
                    CurrentTags.Remove(currentTag.Key);
                    OnPropertyChanged("TagsSelected");
                }
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
            // use transaction 'coz there'll be multiple changes with idea, tags and ideatags
            using (TransactionScope ts = new TransactionScope())
            {
                using (IUnitOfWork transaction = DbFactory.GetUnitOfWork())
                {
                    foreach (KeyValuePair<string, int?> tag in CurrentTags.Where(v => v.Value == null))
                    {
                        // TODO: Enhance this to be transaction compliant, esp if changed to a multi-user system
                        if (AllTags.Count(t => t.Value.Equals(tag.Key, StringComparison.CurrentCultureIgnoreCase)) == 0)
                        {
                            // New tag, insert it
                            transaction.TagRepo.Insert(new Tag { TagName = tag.Key });
                        }
                    }

                    // Initial save of tags. TODO: This may not be required if the inserted tags are just added to collection
                    transaction.Commit();

                    // reload all tags
                    AllTags.Clear();
                    IEnumerable<Tag> tags = transaction.TagRepo.GetByQuery();
                    foreach (Tag tag in tags)
                        AllTags.Add(tag.TagId, tag.TagName);

                    // Insert/Update Idea
                    if (IdeaId.HasValue)
                        transaction.IdeaRepo.Update(CurrentIdea);
                    else
                        transaction.IdeaRepo.Insert(CurrentIdea);

                    // Delete from IdeaTags
                    foreach (KeyValuePair<int, string> tag in DeletedTags)
                    {
                        IdeaTag itemToDelete = transaction.IdeaTagRepo.GetByQuery(it => it.IdeaId == CurrentIdea.IdeaId && it.TagId == tag.Key).FirstOrDefault();
                        if (itemToDelete != null)
                            transaction.IdeaTagRepo.Delete(itemToDelete);
                    }

                    // Insert into IdeaTags
                    foreach (KeyValuePair<string, int?> tag in CurrentTags.Where(v => v.Value == null))
                    {
                        KeyValuePair<int, string> newTag = AllTags.Where(t => t.Value.Equals(tag.Key, StringComparison.CurrentCultureIgnoreCase)).Single();
                        transaction.IdeaTagRepo.Insert(new IdeaTag { IdeaId = CurrentIdea.IdeaId, TagId = newTag.Key });
                    }

                    transaction.Commit();
                }

                ts.Complete();
            }

            if (this.LastVM != null)
                this.LastVM.NavigateTo();
        }
    }
}
